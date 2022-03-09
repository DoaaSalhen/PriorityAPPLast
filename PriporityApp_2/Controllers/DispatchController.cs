using @enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using PriporityApp_2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Dispatch")]
    public class DispatchController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IStateService _stateService;
        private readonly IOrderService _orderService;
        private readonly ITerritoryService _territoryService;
        private readonly IZoneService _zoneService;
        private readonly IDeliveryCustomerService _deliveryCustomerService;
        private readonly IItemService _itemService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IExcelService _excelService;
        private readonly ILogger<DispatchController> _logger;
        private readonly IHubContext<NotificationHub> _hub;


        GeoFilterModel geoFilterModel = new GeoFilterModel { };

        public DispatchController(IRegionService regionService,
                                IStateService stateService,
                                IOrderService orderService,
                                ITerritoryService territoryService,
                                IZoneService zoneService,
                                IDeliveryCustomerService deliveryCustomerService,
                                IItemService itemService,
                                ISubmitNotificationService submitNotificationService,
                               UserManager<AspNetUser> userManager,
                               IExcelService excelService,
                               IHubContext<NotificationHub> hub,
                               ILogger<DispatchController> logger,
                               IUserNotificationService userNotificationService)
        {
            _regionService = regionService;
            _stateService = stateService;
            _orderService = orderService;
            _territoryService = territoryService;
            _zoneService = zoneService;
            _deliveryCustomerService = deliveryCustomerService;
            _itemService = itemService;
            _userManager = userManager;
            _excelService = excelService;
            _logger = logger;
            _userNotificationService = userNotificationService;
        }

        List<ItemModel> itemModels = new List<ItemModel>();
        List<DispatchCaseModel> dispatchCaseModels = new List<DispatchCaseModel>
            {
                new DispatchCaseModel {Id = 1, Name = "ALL"},
                new DispatchCaseModel {Id = 2, Name = "Dispatched"},
                new DispatchCaseModel {Id = 3, Name = "Not Dispatched"},

            };
        List<UserNotificationModel> userNotificationModels = new List<UserNotificationModel>();
        // GET: DispatchController
        public async Task<ActionResult> Index()
        {
            AspNetUser applicationUser =  _userManager.GetUserAsync(User).Result;
            var subRegionModels = _regionService.GetAllISubRegions().Result;
            subRegionModels.Insert(0, new SubRegionModel { Id = -2, Name = "All" });
            geoFilterModel.SubRegions = subRegionModels;
            itemModels = _itemService.GetAllItems().Result;
            itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
            geoFilterModel.Items = itemModels;
            geoFilterModel.ItemSelectedId = -1;
            geoFilterModel.SubRegions = subRegionModels;
            geoFilterModel.SubRegionSelectedId = -1;
            List<StateModel> stateModels = new List<StateModel>();
            stateModels.Insert(0, new StateModel { Id = -1, Name = "select State" });
            geoFilterModel.States = stateModels;
            geoFilterModel.StateSelectedId = -1;
            geoFilterModel.SelectedPriorityDate = DateTime.Today;
            geoFilterModel.ToSelectedPriorityDate = DateTime.Today;
            geoFilterModel.DispatchCases = dispatchCaseModels;
            userNotificationModels =  _userNotificationService.GetAllUnseenNotificationsForUser(applicationUser.Id);
            geoFilterModel.userNotificationModels = userNotificationModels;
            if(geoFilterModel.userNotificationModels.Count > 0)
            {
                geoFilterModel.LastSubmitNotificationId = userNotificationModels.Max(u => u.submitNotificationId);

            }
            return View(geoFilterModel);
        }

        public JsonResult StateFilter(int id)
        {
            var stateModels = new List<StateModel>();
            if (id == -2)
            {
                //stateModels = _stateService.GetAllStates().Result;
                stateModels.Insert(0, new StateModel { Id = -2, Name = "All" });
            }
            else
            {
                stateModels = _stateService.GetStatesBySubRegionId(id).Result;
                stateModels.Insert(0, new StateModel { Id = -2, Name = "All" });

            }
            return Json(new SelectList(stateModels, "Id", "Name"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowSubmittedOrdersDispatch(GeoFilterModel Model)
        {
            try
            {
                List<OrderModel2> orderModels = new List<OrderModel2>();
                List<CustomerModel> customerModels = new List<CustomerModel>();
                DateTime selectedPriorityDate = Model.SelectedPriorityDate.Date;
                geoFilterModel.ToSelectedPriorityDate = Model.ToSelectedPriorityDate.Date;

                List<int> territoryIds = new List<int>();
                if(Model.SubRegionSelectedId != -2)
                {
                    if (Model.StateSelectedId != -2)
                    {
                        territoryIds = _territoryService.GetTerritoryIdsByStateId(Model.StateSelectedId).Result;
                    }
                    else
                    {
                        List<StateModel> stateModels = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                        List<int> stateIds = stateModels.Select(s => s.Id).ToList();
                        List<TerritoryModel> territoryModels = _territoryService.GetAllTerritoriesByListOfStateIds(stateIds).Result;
                        territoryIds = territoryModels.Select(t => t.Id).ToList();
                    }

                    List<ZoneModel> zoneModels = _zoneService.GetListOfZonesByTerritoryIds(territoryIds).Result;
                    List<int> zoneIds = zoneModels.Select(z => z.Id).ToList();
                    customerModels = _deliveryCustomerService.GetCutomersByListOfZoneIds(zoneIds).Result;
                    List<long> customerNumbers = customerModels.Select(c => c.Id).ToList();
                    orderModels = _orderService.GetSubmittedOdersByListOfCustomerNumbers(customerNumbers, Model.SelectedPriorityDate, Model.ToSelectedPriorityDate).Result.ToList();
                }
                else
                {
                    orderModels = _orderService.GetSubmittedOdersByPriorityDate(Model.SelectedPriorityDate, Model.ToSelectedPriorityDate).Result.ToList();
                }
                
                if (Model.ItemSelectedId != -1)
                {
                    orderModels = orderModels.Where(o => o.ItemId == Model.ItemSelectedId).ToList();

                }
               
                    if(Model.DispatchCaseSelectedId == (int)CommanData.DispatchMenu.Dispatched)
                {
                    orderModels = orderModels.Where(o => o.Dispatched == true).ToList();

                }
                else if(Model.DispatchCaseSelectedId == (int)CommanData.DispatchMenu.NotDispatched)
                {
                    orderModels = orderModels.Where(o => o.Dispatched == false).ToList();
                }
                Model.ordersQuantitySum = (float) orderModels.Sum(o => o.PriorityQuantity);
                Model.OrderModel = new OrderModel();
                Model.OrderModel.orders = orderModels;
                Model.Customers = customerModels;
                Model.SubRegions = _regionService.GetAllISubRegions().Result;
                Model.SubRegions.Insert(0, new SubRegionModel { Id = -2, Name = "All" });
                Model.States = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                Model.States.Insert(0, new StateModel { Id = -2, Name = "All" });
                Model.SubRegionSelectedId = -1;
                itemModels = _itemService.GetAllItems().Result;
                itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
                Model.Items = itemModels;
                Model.ItemSelectedId = -1; Model.DispatchCases = dispatchCaseModels;

                return View("Index", Model);
            }
            catch
            {
                return RedirectToAction("ERROR404");
            }

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
            updateModel.Dispatched = false;
            updateModel.Submitted = false;
            bool updateOrderResult = _orderService.UpdateOrder(updateModel).Result;
            if (updateOrderResult == true)
            {

            }
            else
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDispatchedOrders(GeoFilterModel Model)
        {
            try
            {
                bool updateOrderResult = false;
                int SavedOrderCount = 0;
                bool AllOrdersSaved = true;
                foreach (var orderModel in Model.OrderModel.orders)
                {

                    OrderModel2 updateModel = _orderService.GetOrder((long)orderModel.Id);
                    updateModel.Dispatched = orderModel.Dispatched;
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
                ActionResult action = ShowSubmittedOrdersDispatch(Model);
                return action;
            }
            catch(Exception e)
            {
                return RedirectToAction("ERROR404");
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadOrdersForSpecificNotification(long id)
        {
            try
            {


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("ERROR404");
            }
        }





        // GET: DispatchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DispatchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DispatchController/Create
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

        // GET: DispatchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DispatchController/Edit/5
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

        // GET: DispatchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DispatchController/Delete/5
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
