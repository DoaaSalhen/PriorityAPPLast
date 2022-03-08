using @enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PriorityApp.Controllers.CustomerService;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Comman;
using PriorityApp.Service.Contracts.Dispatch;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.Dispatch;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers
{
    public class OrderCommanController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IStateService _stateService;
        private readonly IOrderService _orderService;
        private readonly ITerritoryService _territoryService;
        private readonly IZoneService _zoneService;
        private readonly IDeliveryCustomerService _deliveryCustomerService;
        private readonly IHoldService _holdService;
        private readonly IItemService _itemService;
        private readonly ISubmitNotificationService _submitNotificationService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IExcelService _excelService;
        private readonly ILogger<CSDeliveryOrderController> _logger;
        private readonly IHubContext<NotificationHub> _hub;

        GeoFilterModel geoFilterModel = new GeoFilterModel { };

        public OrderCommanController(IRegionService regionService,
                                IStateService stateService,
                                IOrderService orderService,
                                ITerritoryService territoryService,
                                IZoneService zoneService,
                                IDeliveryCustomerService deliveryCustomerService,
                                IHoldService holdService,
                                IItemService itemService,
                                ISubmitNotificationService submitNotificationService,
                               UserManager<AspNetUser> userManager,
                               IExcelService excelService,
                               ILogger<CSDeliveryOrderController> logger,
                               IHubContext<NotificationHub> hub)
        {
            _regionService = regionService;
            _stateService = stateService;
            _orderService = orderService;
            _territoryService = territoryService;
            _zoneService = zoneService;
            _deliveryCustomerService = deliveryCustomerService;
            _holdService = holdService;
            _itemService = itemService;
            _submitNotificationService = submitNotificationService;
            _userManager = userManager;
            _excelService = excelService;
            _logger = logger;
            _hub = hub;
        }

        List<ItemModel> itemModels = new List<ItemModel>();

        GeoFilterModel StartData()
        {
            try
            {
                GeoFilterModel geoFilterModel = new GeoFilterModel { };
                List<ItemModel> itemModels = new List<ItemModel>();

                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                geoFilterModel.SubRegions = subRegionModels;
                itemModels = _itemService.GetAllItems().Result;
                geoFilterModel.Items = itemModels;

                geoFilterModel.SubRegions = subRegionModels;

                geoFilterModel.SelectedPriorityDate = DateTime.Today;
                return geoFilterModel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;

        }














        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveOrders(GeoFilterModel Model)
        {
            AspNetUser applicationUser = await _userManager.GetUserAsync(User);
            List<string> roles = (List<string>)_userManager.GetRolesAsync(applicationUser).Result;
            try
            {
                bool updateOrderResult = false;
                int SavedOrderCount = 0;
                bool AllOrdersSaved = true;
                foreach (var orderModel in Model.OrderModel.orders.Where(o => o.PriorityId != null && o.Submitted == false))
                {
                    OrderModel2 updateModel = _orderService.GetOrder((long)orderModel.Id);
                    updateModel.PriorityId = orderModel.PriorityId;
                    updateModel.PriorityQuantity = orderModel.PriorityQuantity;
                    updateModel.SavedBefore = true;
                    updateModel.WHSavedID = applicationUser.Id;
                    updateModel.Comment = orderModel.Comment;
                    updateModel.OrderCategoryId =(int) CommanData.OrderCategory.Delivery;
                    //if(orderModel.PriorityId == (int) CommanData.Priorities.Norm)
                    //{
                    //    Model.HoldModel.ReminingQuantity = Model.HoldModel.ReminingQuantity - (int)orderModel.PriorityQuantity;

                    //}
                    updateOrderResult = _orderService.UpdateOrder(updateModel).Result;
                    if (updateOrderResult == true)
                    {
                        SavedOrderCount = SavedOrderCount + 1;
                    }
                    else
                    {
                        AllOrdersSaved = false;
                    }
                }
                if (AllOrdersSaved)
                {
                    await _holdService.UpdateHold(Model.HoldModel);
                }
                TempData["SubmittedOrdersCount"] = SavedOrderCount;
                if (roles.Contains("Sales"))
                {
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    return RedirectToAction("Index", "CSDeliveryOrder");
                }    

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            if (roles.Contains("Sales"))
            {
                return RedirectToAction("Index", "Order");
            }
            else
            {
                return RedirectToAction("Index", "CSDeliveryOrder");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitOrdersForSalesman()
        {
            bool submitted = false;
            try
            {
                //AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
                //List<OrderModel2> unSubmittedOrders = _orderService.GetAllUnSubmittedOrdersByUserId(applicationUser.Id, submitted).Result;

                //var territoryModel = _territoryService.GetTerritoryByUserId(applicationUser.Id);
                //SubmittInfo info = new SubmittInfo();
                //SubmittedOrdersTerritories submittedOrdersTerritories = new SubmittedOrdersTerritories();

                //submittedOrdersTerritories.territorryModel = territoryModel;
                //submittedOrdersTerritories.NumberOfOrders = unSubmittedOrders.Count;
                //List<SubmittedOrdersTerritories> ListOfSubmittedOrdersTerritories = new List<SubmittedOrdersTerritories>();
                //ListOfSubmittedOrdersTerritories.Add(submittedOrdersTerritories);
                //info.submittedOrdersTerritories = ListOfSubmittedOrdersTerritories;
                //info.ordersTosubmit = unSubmittedOrders;
                //info.OrdersCount = unSubmittedOrders.Count();
                //return View(@"Orders\SubmitView", info);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }

        public async Task<ActionResult> Confirm(SubmittInfo model)
        {
            AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
            List<string> roles = (List<string>)_userManager.GetRolesAsync(applicationUser).Result;

            try
            {
                bool result = true;
                string Message = "";
                int submitOderCount = 0;
                bool submitted = false;
                List<long> submittedOrderIdsList = new List<long>();
                List<OrderModel2> unSubmittedOrders = new List<OrderModel2>();
                if (roles.Contains("CustomerService") || roles.Contains("Admin") || roles.Contains("SuperAdmin"))
                {
                    unSubmittedOrders = _orderService.GetAllUnSubmittedOrdersByRole(roles, submitted).Result;
                }
                else
                {
                    unSubmittedOrders = _orderService.GetAllUnSubmittedOrdersByUserId(applicationUser.Id, submitted).Result;
                }
                int lastSubmitNumber = _orderService.getLastSubmitNumberForToday(DateTime.Now);
                string status = "";
                if (lastSubmitNumber >= 1 || lastSubmitNumber == (int)CommanData.NonExistSubmitNumber.ThereOneSubmitForThisPriorityDate)
                {
                    lastSubmitNumber = lastSubmitNumber + 1;
                    status = "N" + lastSubmitNumber;
                }
                else if (lastSubmitNumber == (int)CommanData.NonExistSubmitNumber.NoSubmitBeforeForThisPriorityDate)
                {
                    status = " ";
                }
                else
                {
                    Message = "there is an error";
                }
                foreach (OrderModel2 order in unSubmittedOrders)
                {
                    if (order.Status != null)
                    {
                        order.Status = order.Status.Replace('N', 'M');
                    }
                    else
                    {
                        order.Status = status;
                    }
                    order.SubmitTime = DateTime.Now;
                    order.WHSubmittedID = (string)applicationUser.Id;
                    order.Submitted = true;
                    order.SubmitNumber = lastSubmitNumber;
                    result = _orderService.UpdateOrder(order).Result;

                    if (result == true)
                    {
                        submitOderCount += 1;
                        submittedOrderIdsList.Add(order.Id);
                    }
                }
                if (submitOderCount > 0)
                {
                    SubmitNotificationModel NewsubmitNotificationModel = AddNewSubmitNotification(submittedOrderIdsList);
                    if(NewsubmitNotificationModel != null)
                    {
                        List<SubmitNotificationModel> submitNotificationModels = _submitNotificationService.GetUnseenNotifications();
                        await _hub.Clients.All.SendAsync("SubmitNotification", NewsubmitNotificationModel.Message, 1, NewsubmitNotificationModel.Id);
                    }
                   
                    if (roles.Contains("Sales"))
                    {
                        return RedirectToAction("Index", "Order");
                    }
                    else
                    {
                        return RedirectToAction("Index", "CSDeliveryOrder");
                    }

                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

            }
            if (roles.Contains("Sales"))
            {
                return RedirectToAction("Index", "Order");
            }
            else
            {
                return RedirectToAction("Index", "CSDeliveryOrder");
            }
        }



        public SubmitNotificationModel AddNewSubmitNotification(List<long> submittedOrderIdsList)
        {
            try
            {
                SubmitNotificationModel submitNotificationModel = new SubmitNotificationModel();
                submitNotificationModel.NumberOfSubmittedOrders = submittedOrderIdsList.Count;
                submitNotificationModel.Message = "There are " + submittedOrderIdsList.Count + " new submitted order";
                submitNotificationModel.CreatedDate = DateTime.Now;
                submitNotificationModel.UpdatedDate = DateTime.Now;
                submitNotificationModel.Seen = false;
                SubmitNotificationModel NewsubmitNotificationModel = _submitNotificationService.CreateSubmitNotification(submitNotificationModel);
                if (NewsubmitNotificationModel != null)
                {
                    foreach (var id in submittedOrderIdsList)
                    {
                        OrderNotificationModel orderNotificationModel = new OrderNotificationModel();
                        orderNotificationModel.submitNotificationId = NewsubmitNotificationModel.Id;
                        orderNotificationModel.OrderId = id;
                        orderNotificationModel.CreatedDate = DateTime.Now;
                        orderNotificationModel.UpdatedDate = DateTime.Now;
                        bool addOrderNotification = _submitNotificationService.CreateOrderNotification(orderNotificationModel).Result;
                    }
                }
                return NewsubmitNotificationModel;
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return null;
        }


























        // GET: OrderCommanController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderCommanController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderCommanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderCommanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderCommanController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderCommanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderCommanController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderCommanController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
