using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Dispatch;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriorityApp.Service.Contracts.Comman;
using Microsoft.AspNetCore.SignalR;
using @enum;
using PriorityApp.Service.Models.Dispatch;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using PriporityApp_2.Controllers;

namespace PriorityApp.Controllers.CustomerService
{
    [Authorize(Roles = "SuperAdmin, Admin, CustomerService, Sales")]
    public class WarehouseController : BaseController
    {

        public readonly IRegionService _regionService;
        private readonly IStateService _stateService;
        private readonly IOrderService _orderService;
        private readonly ITerritoryService _territoryService;
        private readonly IZoneService _zoneService;
        private readonly IDeliveryCustomerService _deliveryCustomerService;
        private readonly IHoldService _holdService;
        private readonly IWarehouseOrderHoldService _warehouseOrderHoldService;
        private readonly IItemService _itemService;
        private readonly IPriorityService _priorityService;
        private readonly IWarehouseService _warehouseService;
        private readonly ISubmitNotificationService _submitNotificationService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IExcelService _excelService;
        private readonly ILogger<CSDeliveryOrderController> _logger;
        private readonly IHubContext<NotificationHub> _hub;
        private readonly IEmailSender _emailSender;


        public WarehouseController(
                                IRegionService regionService,
                                IStateService stateService,
                                IOrderService orderService,
                                ITerritoryService territoryService,
                                IZoneService zoneService,
                                IDeliveryCustomerService deliveryCustomerService,
                                IHoldService holdService,
                                IItemService itemService,
                                IPriorityService priorityService,
                                IWarehouseService warehouseService,
                                ISubmitNotificationService submitNotificationService,
                                IUserNotificationService userNotificationService,
                               UserManager<AspNetUser> userManager,
                               IExcelService excelService,
                               ILogger<CSDeliveryOrderController> logger,
                               IHubContext<NotificationHub> hub,
                               IEmailSender emailSender,
                               IWarehouseOrderHoldService warehouseOrderHoldService)
        {
            _regionService = regionService;
            _stateService = stateService;
            _orderService = orderService;
            _territoryService = territoryService;
            _zoneService = zoneService;
            _deliveryCustomerService = deliveryCustomerService;
            _holdService = holdService;
            _itemService = itemService;
            _priorityService = priorityService;
            _warehouseService = warehouseService;
            _submitNotificationService = submitNotificationService;
            _userNotificationService = userNotificationService;
            _userManager = userManager;
            _excelService = excelService;
            _logger = logger;
            _hub = hub;
            _warehouseOrderHoldService = warehouseOrderHoldService;
        }

        // GET: WarehouseController

        public ActionResult AssignWarehouseOrder()
        {
            try
            {
                WarehouseOrderModel warehouseOrderModel = new WarehouseOrderModel();
                List<ItemModel> itemModels = new List<ItemModel>();
                List<WarehouseModel2> warehouseModel2s = new List<WarehouseModel2>();
                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                warehouseOrderModel.SubRegions = subRegionModels;
                itemModels = _itemService.GetItemsByType("Bags").Result;
                warehouseOrderModel.SubRegionSelectedId = -1;
                warehouseOrderModel.SelectedPriorityDate = DateTime.Today;
                warehouseOrderModel.WarehouseModel2 = warehouseModel2s;
                warehouseOrderModel.Items = itemModels;
                warehouseOrderModel.HoldModel = null;
                return View(warehouseOrderModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
                _logger.LogError(e.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignWarehouseOrder(WarehouseOrderModel model)
        {
            try
            {
                TerritoryModel territoryModel = _territoryService.GetTerritory(model.TerritorySelectedId);
                HoldModel holdModel = _holdService.GetHold(model.SelectedPriorityDate, territoryModel.userId);
                if (holdModel != null)
                {
                    model.HoldModel = holdModel;
                    model.holdTotalAssignedQuantity = model.HoldModel.QuotaQuantity - model.HoldModel.ReminingQuantity;
                    model.WarehouseModels = _warehouseService.GetAllWarehouse().Result.ToList();
                    List<WarehouseModel2> warehouseModel2s = new List<WarehouseModel2>();
                    for (int index = 0; index < model.WarehouseModels.Count; index++)
                    {
                        WarehouseModel2 warehouseModel2 = new WarehouseModel2();
                        warehouseModel2.itemModels = _itemService.GetItemsByType("Bags").Result.ToList();
                        warehouseModel2.priorityModels = _priorityService.GetAllPriorities().Result.ToList();
                        warehouseModel2.WarehouseModels = _warehouseService.GetAllWarehouse().Result.ToList();

                        warehouseModel2s.Add(warehouseModel2);
                    }
                    model.WarehouseModel2 = warehouseModel2s;
                    model.Items = _itemService.GetItemsByType("Bags").Result.ToList();
                    model.SubRegions = _regionService.GetAllISubRegions().Result;

                    model.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -2, Name = "Select SubRegion" });
                    model.States = _stateService.GetStatesBySubRegionId(model.SubRegionSelectedId).Result;
                    model.States.Insert(0, new StateModel { Id = -1, Name = "Select State" });

                    model.Territories = _territoryService.GetAllTerritoriesByStateId(model.StateSelectedId).Result;
                    model.Territories.Insert(0, new TerritoryModel { Id = -1, Name = "Select Territory" });
                }
                else
                {
                    ViewBag.Error = "There is no enough quantity";
                }
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveOrder(WarehouseOrderModel model)
        {
            try
            {
                float totalQuantity = 0;
                string Message = "";
                bool addResult = false;
                bool enoughQuantity = true;
                int submitOderCount = 0;
                WarehouseOrderHoldModel warehouseOrderHoldModel = new WarehouseOrderHoldModel();
                AspNetUser applicationUser = await _userManager.GetUserAsync(User);
                OrderModel2 orderModel2 = new OrderModel2();
                TerritoryModel territoryModel = _territoryService.GetTerritory(model.TerritorySelectedId);
                var hold = _holdService.GetHold(model.HoldModel.PriorityDate, territoryModel.userId);
                if (hold != null)
                {
                    //int lastSubmitNumber = _orderService.getLastSubmitNumberForToday(DateTime.Now);
                    //string status = "";
                    //if (lastSubmitNumber >= 1 || lastSubmitNumber == (int)CommanData.NonExistSubmitNumber.ThereOneSubmitForThisPriorityDate)
                    //{
                    //    lastSubmitNumber = lastSubmitNumber + 1;
                    //    status = "N" + lastSubmitNumber;
                    //}
                    //else if (lastSubmitNumber == (int)CommanData.NonExistSubmitNumber.NoSubmitBeforeForThisPriorityDate)
                    //{
                    //    status = " ";
                    //    lastSubmitNumber = 0;
                    //}
                    //else
                    //{
                    //    Message = "there is an error";
                    //}
                    orderModel2.OrderNumber = 0;
                    //orderModel2.CustomerId = model.
                    orderModel2.PriorityDate = model.SelectedPriorityDate;
                    orderModel2.WHSavedID = applicationUser.Id;
                    orderModel2.SavedBefore = true;
                    //orderModel2.WHSubmittedID = applicationUser.Id;
                    //orderModel2.SubmitTime = DateTime.Now;
                    orderModel2.Submitted = false;
                    //orderModel2.SubmitNumber = lastSubmitNumber;
                    //orderModel2.Status = status;
                    orderModel2.OrderCategoryId = (int)CommanData.OrderCategory.Warehouse;
                    foreach (var warehouseOrder in model.WarehouseModel2.Where(w=>w.PrioritySelectedId !=(int) CommanData.Priorities.No))
                     {
                        orderModel2.PriorityId = warehouseOrder.PrioritySelectedId;
                        orderModel2.Comment = warehouseOrder.Comment;
                        orderModel2.CustomerId = warehouseOrder.WarehouseSelectedId;
                        if (warehouseOrder.PrioritySelectedId != 4)
                        {
                            totalQuantity = warehouseOrder.itemModels.Sum(w => w.Quantity);
                            if (totalQuantity >= hold.ReminingQuantity)
                            {
                                enoughQuantity = false;
                            }

                            if (enoughQuantity)
                            {
                                foreach (var item in warehouseOrder.itemModels.Where(i => i.Quantity != 0))
                                {
                                    //if(item.Quantity != 0)
                                    //{
                                    orderModel2.ItemId = item.Id;
                                    orderModel2.PriorityQuantity = item.Quantity;
                                    hold.ReminingQuantity = hold.ReminingQuantity - item.Quantity;
                                    hold.TempReminingQuantity = hold.TempReminingQuantity - item.Quantity;
                                    OrderModel2 newOrderModel = await _orderService.CreateOrder(orderModel2, hold);
                                    submitOderCount = newOrderModel !=null ? submitOderCount++ : submitOderCount;
                                    warehouseOrderHoldModel.OrderId = newOrderModel.Id;
                                    warehouseOrderHoldModel.HoldPriorityDate = hold.PriorityDate;
                                    warehouseOrderHoldModel.HolduserId = hold.userId;
                                   await _warehouseOrderHoldService.CreateWarehouseOrderHold(warehouseOrderHoldModel);

                                }
                            }
                        }
                        else if(warehouseOrder.PrioritySelectedId ==4)
                        {
                            foreach (var item in warehouseOrder.itemModels.Where(i => i.Quantity != 0))
                                {
                                    //if(item.Quantity != 0)
                                    //{
                                    orderModel2.ItemId = item.Id;
                                    orderModel2.PriorityQuantity = item.Quantity;
                                    
                                    OrderModel2 newOrderModel = await _orderService.CreateOrder(orderModel2);
                                    submitOderCount = newOrderModel !=null ? submitOderCount++ : submitOderCount;
                                    warehouseOrderHoldModel.OrderId = newOrderModel.Id;
                                    warehouseOrderHoldModel.OrderId1 = newOrderModel.Id;
                                    warehouseOrderHoldModel.HoldPriorityDate = hold.PriorityDate;
                                    warehouseOrderHoldModel.HolduserId = hold.userId;
                                    await _warehouseOrderHoldService.CreateWarehouseOrderHold(warehouseOrderHoldModel);
                            }

                        }
                        
                    }
                    //if (addResult)
                    //{
                    //    SubmitNotificationModel submitNotificationModel = new SubmitNotificationModel();
                    //    submitNotificationModel.NumberOfSubmittedOrders = submitOderCount;
                    //    submitNotificationModel.Message = "There are " + submitOderCount + " new submitted order";
                    //    submitNotificationModel.CreatedDate = DateTime.Now;
                    //    submitNotificationModel.UpdatedDate = DateTime.Now;
                    //    submitNotificationModel.Seen = false;
                    //    SubmitNotificationModel NewsubmitNotificationModel = _submitNotificationService.CreateSubmitNotification(submitNotificationModel);
                    //    if (NewsubmitNotificationModel != null)
                    //    {
                    //        List<UserNotificationModel> userNotificationModels = new List<UserNotificationModel>();
                    //        List<AspNetUser> users = _userManager.GetUsersInRoleAsync("Dispatch").Result.ToList();
                    //        foreach (var user in users)
                    //        {
                    //            UserNotificationModel userNotificationModel = new UserNotificationModel();
                    //            userNotificationModel.submitNotificationId = NewsubmitNotificationModel.Id;
                    //            userNotificationModel.Seen = false;
                    //            userNotificationModel.userId = user.Id;
                    //            userNotificationModels.Add(userNotificationModel);
                    //        }
                    //        await _userNotificationService.CreateUserNotification(userNotificationModels);

                    //    }
                    //    //List<SubmitNotificationModel> submitNotificationModels = _submitNotificationService.GetUnseenNotifications();
                    //    await _hub.Clients.All.SendAsync("SubmitNotification", "There are new submitted  orders", 1, NewsubmitNotificationModel.Id);
                       // var testMail = await Send("doaa.abdel@ext.cemex.com");
                    //}

                }
                return RedirectToAction("AssignWarehouseOrder");
            }
            catch(Exception e)
            {
                return RedirectToAction("ERROR404");
            }
           // return View("AssignWarehouseOrder");
        }
        //public async Task<IActionResult> Send(string toAddress)
        //{
        //    var subject = "test mail";
        //    var body = "new order submit";
        //    //await _emailSender.SendEmailAsync(toAddress, subject, body);
        //    return View();
        //}


        public ActionResult EditWarehouseOrder()
        {
            try
            {
                WarehouseOrderModel warehouseOrderModel = new WarehouseOrderModel();
                List<ItemModel> itemModels = new List<ItemModel>();

                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                warehouseOrderModel.SubRegions = subRegionModels;
                itemModels = _itemService.GetItemsByType("Bags").Result;
                warehouseOrderModel.Items = itemModels;
                warehouseOrderModel.SubRegions = subRegionModels;
                warehouseOrderModel.SubRegionSelectedId = -1;
                warehouseOrderModel.SelectedPriorityDate = DateTime.Today;
                warehouseOrderModel.Priorities = _priorityService.GetAllPriorities().Result.ToList();
                return View(warehouseOrderModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }

        }

        public ActionResult ShowSubmittedOrder()
        {
            try
            {
                WarehouseOrderModel warehouseOrderModel = new WarehouseOrderModel();
                List<ItemModel> itemModels = new List<ItemModel>();
                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                warehouseOrderModel.SubRegions = subRegionModels;
                itemModels = _itemService.GetItemsByType("Bags").Result;
                warehouseOrderModel.Items = itemModels;
                warehouseOrderModel.SubRegions = subRegionModels;
                warehouseOrderModel.SubRegionSelectedId = -1;
                warehouseOrderModel.SelectedPriorityDate = DateTime.Today;
                return View("ShowSubmittedWarehouseOrder", warehouseOrderModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchForOrders(WarehouseOrderModel Model)
        {
            try
            {
                List<OrderModel2> orderModels = new List<OrderModel2>();
                List<CustomerModel> customerModels = new List<CustomerModel>();
                DateTime selectedPriorityDate = Model.SelectedPriorityDate.Date;

                string userId = _territoryService.GetTerritory(Model.TerritorySelectedId).userId;
                HoldModel hold= _holdService.GetLastHoldByUserIdAndPriorityDate(userId, Model.SelectedPriorityDate);
                List <WarehouseOrderHoldModel> warehouseOrderHoldModels = _warehouseOrderHoldService.GetWarehouseOrderHold(userId, Model.SelectedPriorityDate).ToList();
                foreach(var warehouseOrderHoldModel in warehouseOrderHoldModels)
                {
                    warehouseOrderHoldModel.Order = _orderService.GetOrder(warehouseOrderHoldModel.OrderId);
                    //if(Model.ViewCase == "Show")
                    //{
                    //    if (warehouseOrderHoldModel.Order.Submitted == false)
                    //    {
                    //        warehouseOrderHoldModels.Remove(warehouseOrderHoldModel);
                    //    }
                    //}
                    //else 
                    //{
                    //    if (warehouseOrderHoldModel.Order.Submitted == true)
                    //    {
                    //        warehouseOrderHoldModels.Remove(warehouseOrderHoldModel);
                    //    }
                    //}
                }
                Model.warehouseOrderHoldModels = warehouseOrderHoldModels;
                TerritoryModel territoryModel = _territoryService.GetTerritory(Model.TerritorySelectedId);
                Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModel.userId);
                Model.HoldModel = Model.HoldModel;
                Model.holdTotalAssignedQuantity = Model.HoldModel.QuotaQuantity - Model.HoldModel.ReminingQuantity;
                Model.SubRegions = _regionService.GetAllISubRegions().Result;
                //Model.SubRegionSelectedId = -1;
                Model.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -1, Name = "Select Region" });
                Model.States = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                Model.States.Insert(0, new StateModel { Id = -1, Name = "Select State" });
                Model.Priorities = _priorityService.GetAllPriorities().Result.ToList();
                Model.Territories = _territoryService.GetAllTerritoriesByStateId(Model.StateSelectedId).Result;
                Model.Territories.Insert(0, new TerritoryModel { Id = -1, Name = "Select Territory" });
                Model.Items = _itemService.GetItemsByType("Bags").Result;

                if(Model.ViewCase == "Show")
                {
                    return View("ShowSubmittedWarehouseOrder", Model);
                }
                else
                {
                    return View("EditWarehouseOrder", Model);

                }
            }
            catch(Exception e)
            {
                return RedirectToAction("ERROR404");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditOrders(WarehouseOrderModel Model)
        {
            AspNetUser applicationUser = await _userManager.GetUserAsync(User);
            try
            {
                bool updateOrderResult = false;
                int SavedOrderCount = 0;
                bool AllOrdersSaved = true;
                HoldModel DBholdModel = _holdService.GetHold(Model.HoldModel.PriorityDate, Model.HoldModel.userId);

                foreach (var warehouseModel in Model.warehouseOrderHoldModels)
                {
                    OrderModel2 updateModel = _orderService.GetOrder((long)warehouseModel.Order.Id);
                    if (warehouseModel.Order.PriorityId != (int)CommanData.Priorities.No && updateModel.Submitted == false)
                      {
                        if (warehouseModel.Order.SavedBefore == true)
                        {
                            if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                            {
                                float changeRate = (float)updateModel.PriorityQuantity - (float)warehouseModel.Order.PriorityQuantity;
                                DBholdModel.ReminingQuantity = DBholdModel.ReminingQuantity + changeRate;

                            }
                            else if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                            {
                                DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)warehouseModel.Order.PriorityQuantity;
                                DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity - (float)warehouseModel.Order.PriorityQuantity;

                            }
                            else if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                            {
                                DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)updateModel.PriorityQuantity;
                                DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)updateModel.PriorityQuantity;

                            }
                            else if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                            {
                                float changeRate = (float)updateModel.PriorityQuantity - (float)warehouseModel.Order.PriorityQuantity;
                                DBholdModel.ExtraQuantity = DBholdModel.ExtraQuantity + changeRate;

                            }
                            else if (warehouseModel.Order.PriorityId== (int)CommanData.Priorities.No && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                            {
                                DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity + (float)updateModel.PriorityQuantity;
                            }
                            else if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.No && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                            {
                                DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity - (float)updateModel.PriorityQuantity;
                            }
                            else if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.No)
                            {
                                DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)updateModel.PriorityQuantity;
                            }
                            else if (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.No)
                            {
                                DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)updateModel.PriorityQuantity;
                            }
                        }
                    }
                    else if (warehouseModel.Order.SavedBefore == false && (warehouseModel.Order.PriorityId == (int)CommanData.Priorities.Norm))
                    {
                        DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)warehouseModel.Order.PriorityQuantity;

                    }
                    if (warehouseModel.Order.PriorityId != (int)CommanData.Priorities.No)
                    {
                        DBholdModel.TempReminingQuantity = DBholdModel.ReminingQuantity;
                        updateModel.ItemId = warehouseModel.Order.ItemId;
                        updateModel.PriorityId = warehouseModel.Order.PriorityId;
                        updateModel.PriorityQuantity = warehouseModel.Order.PriorityQuantity;
                        updateModel.SavedBefore = true;
                        updateModel.WHSavedID = applicationUser.Id;
                        updateModel.Comment = warehouseModel.Order.Comment;
                        updateModel.Truck = warehouseModel.Order.Truck;
                        updateModel.OrderCategoryId = (int)CommanData.OrderCategory.Warehouse;
                    }
                    else if (updateModel.SavedBefore == true && warehouseModel.Order.PriorityId == (int)CommanData.Priorities.No)
                    {

                        updateModel.PriorityId = warehouseModel.Order.PriorityId;
                        updateModel.SavedBefore = false;
                        updateModel.Truck = "";
                        updateModel.WHSavedID = applicationUser.Id;
                        updateModel.PriorityQuantity = 0;

                    }


                    updateOrderResult = _orderService.UpdateOrder2(updateModel, DBholdModel).Result;
                    if (updateOrderResult == true)
                    {
                        SavedOrderCount = SavedOrderCount + 1;
                    }
                    else
                    {
                        AllOrdersSaved = false;
                    }
                }
                TempData["SubmittedOrdersCount"] = SavedOrderCount;
                if (Model.orderType == (int)CommanData.OrderCategory.Delivery)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("EditPickUpOrders", "PickUpOrders");
                }
            }
            catch (Exception e)
            {

                _logger.LogError(e.ToString());
                return RedirectToAction("ERROR404");
            }
            //return RedirectToAction("Index");
        }


        public async Task<ActionResult> UnLockOrder(long id)
        {
            OrderModel2 Model = _orderService.GetOrder(id);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnLockOrder(long id, IFormCollection collection)
        {
            OrderModel2 updateModel = _orderService.GetOrder(id);
            updateModel.Submitted = false;
            bool updateOrderResult = _orderService.UpdateOrder(updateModel).Result;
            if (updateOrderResult == true)
            {

            }
            else
            {
            }
            return RedirectToAction("ShowSubmittedOrder");
        }




        public ActionResult Index()
        {
            return View();
        }

        // GET: WarehouseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WarehouseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WarehouseController/Create
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

        // GET: WarehouseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WarehouseController/Edit/5
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

        // GET: WarehouseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WarehouseController/Delete/5
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
