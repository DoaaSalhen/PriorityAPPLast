using @enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers.CustomerService
{
    [Authorize(Roles = "SuperAdmin, Admin, CustomerService, Sales")]
    public class CSDeliveryOrderController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IStateService _stateService;
        private readonly IOrderService _orderService;
        private readonly ITerritoryService _territoryService;
        private readonly IZoneService _zoneService;
        private readonly IDeliveryCustomerService _deliveryCustomerService;
        private readonly IHoldService _holdService;
        private readonly IItemService _itemService;
        private readonly IPriorityService _priorityService;
        private readonly ISubmitNotificationService _submitNotificationService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IExcelService _excelService;
        private readonly ILogger<CSDeliveryOrderController> _logger;
        private readonly IHubContext<NotificationHub> _hub;
        private readonly IEmailSender _emailSender;
        private readonly IWarehouseOrderHoldService _warehouseOrderHoldService;


        GeoFilterModel geoFilterModel = new GeoFilterModel { };

        public CSDeliveryOrderController(IRegionService regionService,
                                IStateService stateService,
                                IOrderService orderService,
                                ITerritoryService territoryService,
                                IZoneService zoneService,
                                IDeliveryCustomerService deliveryCustomerService,
                                IHoldService holdService,
                                IItemService itemService,
                                IPriorityService priorityService,
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
            _submitNotificationService = submitNotificationService;
            _userNotificationService = userNotificationService;
            _userManager = userManager;
            _excelService = excelService;
            _logger = logger;
            _hub = hub;
            _emailSender = emailSender;
            _warehouseOrderHoldService = warehouseOrderHoldService;
        }

        List<ItemModel> itemModels = new List<ItemModel>();
        public enum Priorities
        {
            No = 2,
            Norm = 3,
            Extra = 4,
        };
        //class submittedInfo
        //{
        //    public string territoryName { get; set; }
        //    public int NumberOfOrders { get; set; }
        //}

        // GET: CSDeliveryOrderController
        public async Task<ActionResult> Index()
        {
            try
            {
                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                geoFilterModel.SubRegions = subRegionModels;
                geoFilterModel.SubRegionSelectedId = -1;
                itemModels = _itemService.GetAllItems().Result;
                itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
                geoFilterModel.Items = itemModels;
                geoFilterModel.ItemSelectedId = -1;

                //geoFilterModel.SubRegions = subRegionModels;

                geoFilterModel.SelectedPriorityDate = DateTime.Today;

                return View(geoFilterModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }
        }
        public JsonResult StateFilter(int id)
        {
            try
            {
                var stateModels = new List<StateModel>();
                if (id == -1)
                {
                    //stateModels = _stateService.GetAllStates().Result;
                    stateModels = null;
                }
                else
                {
                    stateModels = _stateService.GetStatesBySubRegionId(id).Result;
                    stateModels.Insert(0, new StateModel { Id = -1, Name = "select State" });

                }
                return Json(new SelectList(stateModels, "Id", "Name"));
            }
            catch (Exception e)
            {
                return (JsonResult)ERROR404();
            }

        }

        public JsonResult TerritoryFilter(int id)
        {
            var territoryModels = new List<TerritoryModel>();
            if (id == -1)
            {
                //stateModels = _stateService.GetAllStates().Result;
                territoryModels = null;
            }
            else
            {
                territoryModels = _territoryService.GetAllTerritoriesByStateId(id).Result;

                territoryModels.Insert(0, new TerritoryModel { Id = -1, Name = "select Territory" });
            }
            return Json(new SelectList(territoryModels, "Id", "Name"));

        }

        public JsonResult ZoneFilter(int id)
        {
            var zoneModels = new List<ZoneModel>();
            if (id == -1)
            {
                //stateModels = _stateService.GetAllStates().Result;
                zoneModels = null;


            }
            else
            {
                zoneModels = _zoneService.GetListOfZonesByTerritoryId(id);



                zoneModels.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
            }
            return Json(new SelectList(zoneModels, "Id", "Name"));

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchForOrders(GeoFilterModel Model)
        {
            try
            {
                List<OrderModel2> orderModels = new List<OrderModel2>();
                List<CustomerModel> customerModels = new List<CustomerModel>();
                DateTime selectedPriorityDate = Model.SelectedPriorityDate.Date;
                AspNetUser applicationUser = await _userManager.GetUserAsync(User);
                List<TerritoryModel> territoryModelSales = null;
                var roles = _userManager.GetRolesAsync(applicationUser).Result.ToList();
                if(roles.Contains("Sales"))
                {
                     territoryModelSales = _territoryService.GetTerritoryByUserId(applicationUser.Id);
                    Model.SalesTerritoriesSelectedIds = territoryModelSales.Select(t=>t.Id).ToList();
                    Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModelSales.First().userId);
                    Model.Priorities = _priorityService.GetAllPrioritiesExceptExtra().Result.ToList();
                                
                }
                else
                {
                    Model.Priorities = _priorityService.GetAllPriorities().Result.ToList();
                    TerritoryModel territoryModel = _territoryService.GetTerritory(Model.TerritorySelectedId);
                    Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModel.userId);
                    Model.SubRegions = _regionService.GetAllISubRegions().Result;
                    Model.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -1, Name = "Select Region" });
                    Model.States = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                    Model.States.Insert(0, new StateModel { Id = -1, Name = "Select State" });
                    Model.Territories = _territoryService.GetAllTerritoriesByStateId(Model.StateSelectedId).Result;
                    Model.Territories.Insert(0, new TerritoryModel { Id = -1, Name = "Select Territory" });
                }
                itemModels = _itemService.GetAllItems().Result;
                itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
                Model.Items = itemModels;
                //Model.ItemSelectedId = -1;
                if (Model.HoldModel != null)
                {
                    if (Model.ZoneSelectedId > 0)
                    {
                        customerModels = _deliveryCustomerService.GetCutomersByZoneId(Model.ZoneSelectedId).Result;
                        Model.Zones = _zoneService.GetListOfZonesByTerritoryId(Model.TerritorySelectedId);
                        Model.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
                    }
                    else
                    {
                        List<ZoneModel> zoneModels = new List<ZoneModel>();
                        if (roles.Contains("Sales"))
                        {
                            zoneModels = _zoneService.GetListOfZonesByTerritoryIds(Model.SalesTerritoriesSelectedIds).Result;

                        }
                        else
                        {
                            zoneModels = _zoneService.GetListOfZonesByTerritoryId(Model.TerritorySelectedId);

                        }
                        List<int> zoneIds = zoneModels.Select(z => z.Id).ToList();
                        customerModels = _deliveryCustomerService.GetCutomersByListOfZoneIds(zoneIds).Result;
                        Model.Zones = zoneModels;
                        Model.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
                    }

                    List<long> customerNumbers = customerModels.Select(c => c.Id).ToList();
                    //orderModels = _orderService.GetOdersByListOfCustomerNumbers(customerNumbers, selectedPriorityDate).Result.ToList();
                    if (Model.orderType == (int)CommanData.OrderCategory.Delivery)
                    {
                        orderModels = _orderService.GetOdersByListOfCustomerNumbers(customerNumbers, selectedPriorityDate).Result.Where(o => o.OrderCategoryId == (int)CommanData.OrderCategory.Delivery && o.Submitted == false).ToList();

                    }
                    else if (Model.orderType == (int)CommanData.OrderCategory.Pickup && Model.viewCase == "show")
                    {
                        orderModels = _orderService.GetSubmittedOdersByListOfCustomerNumbers(customerNumbers, selectedPriorityDate, selectedPriorityDate).Result.Where(o => o.OrderCategoryId == Model.orderType).ToList();
                    }
                    else if (Model.orderType == (int)CommanData.OrderCategory.Pickup && Model.viewCase == "edit")
                    {
                        orderModels = _orderService.GetOdersByListOfCustomerNumbers(customerNumbers, selectedPriorityDate).Result.Where(o => o.OrderCategoryId == Model.orderType && o.Submitted == false).ToList();
                    }
                    if (Model.ItemSelectedId != -1)
                    {
                        orderModels = orderModels.Where(o => o.ItemId == Model.ItemSelectedId).ToList();

                    }

                    Model.OrderModel = new OrderModel();
                    Model.OrderModel.orders = orderModels.OrderBy(o => o.Customer.CustomerName).ToList();
                    Model.Customers = customerModels;
                    //Model.Priorities = _priorityService.GetAllPriorities().Result.ToList();
                    //TerritoryModel territoryModel = _territoryService.GetTerritory(Model.TerritorySelectedId);
                    //Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModel.userId);
                    Model.OrderModel.holdModel = Model.HoldModel;
                    Model.holdTotalAssignedQuantity = Model.HoldModel.QuotaQuantity - Model.HoldModel.ReminingQuantity;
                    //Model.SubRegions = _regionService.GetAllISubRegions().Result;
                    ////Model.SubRegionSelectedId = -1;

                    //Model.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -1, Name = "Select Region" });
                    //Model.States = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                    //Model.States.Insert(0, new StateModel { Id = -1, Name = "Select State" });

                    //Model.Territories = _territoryService.GetAllTerritoriesByStateId(Model.StateSelectedId).Result;
                    //Model.Territories.Insert(0, new TerritoryModel { Id = -1, Name = "Select Territory" });

                    if (Model.orderType == (int)CommanData.OrderCategory.Delivery)
                    {
                        return View("index", Model);
                    }
                    else
                    {
                        return View(@"PickUpOrders\EditPickUpOrders", Model);
                    }
                }
                else
                {
                    ViewBag.Error = "There Is No Quota for This Priority Date";
                    if (Model.orderType == (int)CommanData.OrderCategory.Delivery)
                    {
                        return View("index",Model);
                    }
                    else
                    {
                        return View(@"PickUpOrders\EditPickUpOrders", Model);
                    }
                }

            }
            catch
            {
                return RedirectToAction("ERROR404");
            }
        }

        public async Task<JsonResult> SaveComment(OrderDetails orderModel)
        {
            try
            {
                OrderModel2 updateModel = _orderService.GetOrder((long)orderModel.Id);
                updateModel.Comment = orderModel.Comment;
                bool updateOrderResult = await _orderService.UpdateOrder(updateModel);
                if(updateOrderResult)
                {
                    return Json(true);

                }

            }
            catch (Exception e)
            {
            }
            return Json(false);

        }

        public async Task<JsonResult> SaveTruck(OrderDetails orderModel)
        {
            try
            {
                OrderModel2 updateModel =  _orderService.GetOrder(orderModel.Id);
                updateModel.Truck = orderModel.Truck;
                bool updateOrderResult = await _orderService.UpdateOrder(updateModel);
                if (updateOrderResult)
                {
                    return Json(true);

                }

            }
            catch (Exception e)
            {
            }
            return Json(false);

        }
        public async Task<JsonResult> SaveOrders2(OrderDetails orderModel)
        {
            AspNetUser applicationUser = await _userManager.GetUserAsync(User);
            try
            {
                bool updateOrderResult = false;
                int SavedOrderCount = 0;
                bool AllOrdersSaved = true;

                HoldModel DBholdModel = _holdService.GetHold(orderModel.PriorityDate, orderModel.UserId);
                OrderModel2 updateModel = _orderService.GetOrder((long)orderModel.Id);
                if (updateModel.SavedBefore == true)
                {
                    float changeRate = (float)orderModel.PriorityQuantity - (float)updateModel.PriorityQuantity;

                    if (orderModel.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                    {
                        DBholdModel.ReminingQuantity = DBholdModel.ReminingQuantity - changeRate;

                    }
                    else if (orderModel.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                    {
                        DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)orderModel.PriorityQuantity - changeRate;
                        DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity - (float)orderModel.PriorityQuantity + changeRate;
                    }
                    else if (orderModel.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                    {
                        DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity + (float)updateModel.PriorityQuantity - changeRate;
                        DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)updateModel.PriorityQuantity + changeRate;
                    }

                    else if (orderModel.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                    {
                        DBholdModel.ExtraQuantity = DBholdModel.ExtraQuantity + changeRate;

                    }
                    else if (orderModel.PriorityId == (int)CommanData.Priorities.No && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                    {
                        DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity + (float)updateModel.PriorityQuantity;
                    }
                    else if (orderModel.PriorityId == (int)CommanData.Priorities.No && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                    {
                        DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity - (float)updateModel.PriorityQuantity;
                    }
                    else if (orderModel.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.No)
                    {
                        DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)updateModel.PriorityQuantity - changeRate;
                    }
                    else if (orderModel.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.No)
                    {
                        DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)updateModel.PriorityQuantity + changeRate;
                    }
                }

                else if (updateModel.SavedBefore == false && (orderModel.PriorityId == (int)CommanData.Priorities.Norm))
                {
                    DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)orderModel.PriorityQuantity;

                }
                else if (updateModel.SavedBefore == false && (orderModel.PriorityId == (int)CommanData.Priorities.Extra))
                {
                    DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)orderModel.PriorityQuantity;

                }
                if (orderModel.PriorityId != (int)CommanData.Priorities.No)
                {
                    DBholdModel.TempReminingQuantity = DBholdModel.ReminingQuantity;
                    updateModel.ItemId = orderModel.ItemId;
                    updateModel.PriorityId = orderModel.PriorityId;
                    updateModel.PriorityQuantity = orderModel.PriorityQuantity;
                    updateModel.SavedBefore = true;
                    updateModel.WHSavedID = applicationUser.Id;
                    updateModel.Comment = orderModel.Comment;
                    updateModel.Truck = orderModel.Truck;
                    updateModel.OrderCategoryId = (int)CommanData.OrderCategory.Delivery;
                }
                else if (updateModel.SavedBefore == true && orderModel.PriorityId == (int)CommanData.Priorities.No)
                {
                    DBholdModel.TempReminingQuantity = DBholdModel.ReminingQuantity;
                    updateModel.PriorityId = orderModel.PriorityId;
                    updateModel.SavedBefore = true;
                    updateModel.Truck = "";
                    updateModel.WHSavedID = applicationUser.Id;
                    updateModel.PriorityQuantity = 0;
                    updateModel.ItemId = orderModel.ItemId;
                    updateModel.OrderCategoryId = (int)CommanData.OrderCategory.Delivery;
                    updateModel.Comment = "";

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
                updateOrderResult = false;
            }
            catch(Exception e)
            {
                return Json(false);

            }
            return Json(true);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveDeliveryOrders(GeoFilterModel Model)
        {
            return RedirectToAction("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveOrders(GeoFilterModel Model)
        {
            AspNetUser applicationUser = await _userManager.GetUserAsync(User);
            try
            {
                bool updateOrderResult = false;
                int SavedOrderCount = 0;
                bool AllOrdersSaved = true;

                foreach (var orderModel in Model.OrderModel.orders)
                {
                    HoldModel DBholdModel = _holdService.GetHold(Model.HoldModel.PriorityDate, Model.HoldModel.userId);
                    OrderModel2 updateModel = _orderService.GetOrder((long)orderModel.Id);
                    if (orderModel.SavedBefore == true)
                    {
                        float changeRate = (float)orderModel.PriorityQuantity - (float)updateModel.PriorityQuantity;

                        if (orderModel.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                        {
                            DBholdModel.ReminingQuantity = DBholdModel.ReminingQuantity - changeRate;

                        }
                        else if (orderModel.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                        {
                            DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)orderModel.PriorityQuantity - changeRate;
                            DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity - (float)orderModel.PriorityQuantity + changeRate;
                        }
                        else if (orderModel.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                        {
                            DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity + (float)updateModel.PriorityQuantity -changeRate;
                            DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)updateModel.PriorityQuantity + changeRate;
                        }

                        else if (orderModel.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                        {
                            DBholdModel.ExtraQuantity = DBholdModel.ExtraQuantity + changeRate;

                        }
                        else if (orderModel.PriorityId == (int)CommanData.Priorities.No && updateModel.PriorityId == (int)CommanData.Priorities.Norm)
                        {
                            DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity + (float)updateModel.PriorityQuantity ;
                        }
                        else if (orderModel.PriorityId == (int)CommanData.Priorities.No && updateModel.PriorityId == (int)CommanData.Priorities.Extra)
                        {
                            DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity - (float)updateModel.PriorityQuantity ;
                        }
                        else if (orderModel.PriorityId == (int)CommanData.Priorities.Norm && updateModel.PriorityId == (int)CommanData.Priorities.No)
                        {
                            DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)updateModel.PriorityQuantity - changeRate;
                        }
                        else if (orderModel.PriorityId == (int)CommanData.Priorities.Extra && updateModel.PriorityId == (int)CommanData.Priorities.No)
                        {
                            DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)updateModel.PriorityQuantity + changeRate;
                        }
                    }

                    else if (orderModel.SavedBefore == false && (orderModel.PriorityId == (int)CommanData.Priorities.Norm))
                    {
                        DBholdModel.ReminingQuantity = (float)DBholdModel.ReminingQuantity - (float)orderModel.PriorityQuantity;

                    }
                    else if (orderModel.SavedBefore == false && (orderModel.PriorityId == (int)CommanData.Priorities.Extra))
                    {
                        DBholdModel.ExtraQuantity = (float)DBholdModel.ExtraQuantity + (float)orderModel.PriorityQuantity;

                    }
                    if (orderModel.PriorityId != (int)CommanData.Priorities.No)
                    {
                        DBholdModel.TempReminingQuantity = DBholdModel.ReminingQuantity;
                        updateModel.ItemId = orderModel.ItemId;
                        updateModel.PriorityId = orderModel.PriorityId;
                        updateModel.PriorityQuantity = orderModel.PriorityQuantity;
                        updateModel.SavedBefore = true;
                        updateModel.WHSavedID = applicationUser.Id;
                        updateModel.Comment = orderModel.Comment;
                        updateModel.Truck = orderModel.Truck;
                        updateModel.OrderCategoryId = Model.orderType;
                    }
                    else if(orderModel.SavedBefore == true && orderModel.PriorityId == (int)CommanData.Priorities.No)
                    {
                        DBholdModel.TempReminingQuantity = DBholdModel.ReminingQuantity;
                        updateModel.PriorityId = orderModel.PriorityId;
                        updateModel.SavedBefore = true;
                        updateModel.Truck = "";
                        updateModel.WHSavedID = applicationUser.Id;
                        updateModel.PriorityQuantity = 0;
                        updateModel.ItemId = orderModel.ItemId;
                        updateModel.OrderCategoryId = Model.orderType;
                        updateModel.Comment = "";

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
                    updateOrderResult = false;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitOrders()
        {
            AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
            bool submitted = false;

            try
            {
                List<string> roles = (List<string>)_userManager.GetRolesAsync(applicationUser).Result;
                List<OrderModel2> unSubmittedOrders = null;
                SubmittInfo info = new SubmittInfo();
                info.holdModels = new List<HoldModel>();
                List<SubmittedOrdersTerritories> submittedOrdersTerritories = new List<SubmittedOrdersTerritories>();
                if (!roles.Contains("Sales"))
                {
                    unSubmittedOrders = _orderService.GetAllUnSubmittedOrdersByRole(roles, submitted).Result;
                }
                else
                {
                    unSubmittedOrders = _orderService.GetSubmittedOdersByUserId(applicationUser.Id, false).Result;

                }
                for (int index=0; index < unSubmittedOrders.Count; index++)
                    {
                        if(unSubmittedOrders[index].OrderCategoryId == (int)CommanData.OrderCategory.Warehouse)
                        {

                            WarehouseOrderHoldModel warehouseOrderHoldModel = _warehouseOrderHoldService.GetWarehouseOrderHold(unSubmittedOrders[index].Id);
                            TerritoryModel WHTerritory = _territoryService.GetTerritory(warehouseOrderHoldModel.TerritoryId);
                            unSubmittedOrders[index].Customer.zone.Territory = WHTerritory;
                        }
                    }
                    //List<long> customerIds = unSubmittedOrders.Select(o => o.CustomerId).ToList();
                    //List<int> zoneIds = _deliveryCustomerService.GetZoneIdsByListOfCustomerIds(customerIds);
                    //List<int> territoryIds = _zoneService.GetListOfTerritoryIdsByZoneIds(zoneIds);
                    //var territoryModels = _territoryService.GetAllTeritories().Result.Where(t => territoryIds.Contains(t.Id)).GroupBy(t => t.Id).ToList();
                    var unsubmittedOrdersGroup = unSubmittedOrders.GroupBy(o => o.Customer.zone.Territory.Id).ToList();
                    //SubmittedOrdersTerritories item = new SubmittedOrdersTerritories();

                    foreach (var unsubmittedOrderGroup in unsubmittedOrdersGroup)
                    {
                        SubmittedOrdersTerritories item = new SubmittedOrdersTerritories();

                        item.territorryModel = _territoryService.GetTerritory(unsubmittedOrderGroup.Key);
                        submittedOrdersTerritories.Add(item);
                        //foreach (var order in unsubmittedOrderGroup)
                        //{
                        //    SubmittedOrdersTerritories item = new SubmittedOrdersTerritories();
                        //    item.territorryModel = order.Customer.zone.Territory;
                        //    submittedOrdersTerritories.Add(item);
                        //}
                    }
                    info.submittedOrdersTerritories = submittedOrdersTerritories;

                
                //else
                //{
                //    unSubmittedOrders = _orderService.GetSubmittedOdersByUserId(applicationUser.Id, false).Result;
                //    //info.SubmittedOrdersTerritory = _territoryService.GetTerritoryByUserId(applicationUser.Id);
                //    for (int index = 0; index < unSubmittedOrders.Count; index++)
                //    {
                //        if (unSubmittedOrders[index].OrderCategoryId == (int)CommanData.OrderCategory.Warehouse)
                //        {

                //            WarehouseOrderHoldModel warehouseOrderHoldModel = _warehouseOrderHoldService.GetWarehouseOrderHold(unSubmittedOrders[index].Id);
                //            TerritoryModel WHTerritory = _territoryService.GetTerritory(warehouseOrderHoldModel.TerritoryId);
                //            unSubmittedOrders[index].Customer.zone.Territory = WHTerritory;
                //        }
                //    }
                //}

                //SubmittInfo info = new SubmittInfo();
                //info.holdModels = new List<HoldModel>();
                //List<SubmittedOrdersTerritories> submittedOrdersTerritories = new List<SubmittedOrdersTerritories>();
                //var unsubmittedOrdersGroup = unSubmittedOrders.GroupBy(o => o.Customer.zone.Territory.userId).ToList();
                //foreach (var territoryModel in territoryModels)
                //{
                //    SubmittedOrdersTerritories item = new SubmittedOrdersTerritories();
                //    item.territorryModel = territoryModel.First();
                //    submittedOrdersTerritories.Add(item);
                //}
                HoldModel holdModel = new HoldModel();
                foreach (var order in unSubmittedOrders)
                {
                    holdModel = _holdService.GetHold(order.PriorityDate, order.Customer.zone.Territory.userId);
                    holdModel.userId = _userManager.FindByIdAsync(order.Customer.zone.Territory.userId).Result.UserName;
                    info.holdModels.Add(holdModel);
                }

                info.holdModels = info.holdModels.GroupBy(h => h.userId).Select(x => x.First()).ToList();
                info.ordersTosubmit = unSubmittedOrders;
                info.OrdersCount = unSubmittedOrders.Count();

                return View("SubmitView", info);

            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
                _logger.LogError(e.ToString());
            }
            //return null;
        }

        [HttpPost]
        public async Task<ActionResult> Confirm(SubmittInfo model)
        {
            AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
            try
            {
                bool result = true;
                string Message = "";
                int submitOderCount = 0;
                bool submitted = false;
                List<long> submittedOrderIdsList = new List<long>();
                List<string> roles = (List<string>)_userManager.GetRolesAsync(applicationUser).Result;
                List<OrderModel2> unSubmittedOrders = _orderService.GetAllUnSubmittedOrdersByRole(roles, submitted).Result;
                int lastSubmitNumber = _orderService.getLastSubmitNumberForToday(DateTime.Today);
                string status = "";
                if (lastSubmitNumber >= 1 || lastSubmitNumber == (int)CommanData.NonExistSubmitNumber.ThereOneSubmitForThisPriorityDate)
                {
                    lastSubmitNumber = lastSubmitNumber + 1;
                    status = "N" + lastSubmitNumber;
                }
                else if (lastSubmitNumber == (int)CommanData.NonExistSubmitNumber.NoSubmitBeforeForThisPriorityDate)
                {
                    status = " ";
                    lastSubmitNumber = 0;
                }
                else
                {
                    Message = "there is an error";
                }
                foreach (OrderModel2 order in unSubmittedOrders)
                {
                    if (order.Status != null)
                    {
                        order.Status = order.Status == " " ? "Modified" : order.Status.Replace('N', 'M');
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
                    SubmitNotificationModel submitNotificationModel = new SubmitNotificationModel();
                    submitNotificationModel.NumberOfSubmittedOrders = submitOderCount;
                    submitNotificationModel.Message = "There are " + submitOderCount + " new submitted order";
                    submitNotificationModel.CreatedDate = DateTime.Now;
                    submitNotificationModel.UpdatedDate = DateTime.Now;
                    submitNotificationModel.Seen = false;
                    SubmitNotificationModel NewsubmitNotificationModel = _submitNotificationService.CreateSubmitNotification(submitNotificationModel);
                    if (NewsubmitNotificationModel != null)
                    {
                        List<UserNotificationModel> userNotificationModels = new List<UserNotificationModel>();
                        List<AspNetUser> users = _userManager.GetUsersInRoleAsync("Dispatch").Result.ToList();
                        foreach (var user in users)
                        {
                            UserNotificationModel userNotificationModel = new UserNotificationModel();
                            userNotificationModel.submitNotificationId = NewsubmitNotificationModel.Id;
                            userNotificationModel.Seen = false;
                            userNotificationModel.userId = user.Id;
                            userNotificationModels.Add(userNotificationModel);
                        }
                        await _userNotificationService.CreateUserNotification(userNotificationModels);
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
                    //List<SubmitNotificationModel> submitNotificationModels = _submitNotificationService.GetUnseenNotifications();
                    await _hub.Clients.All.SendAsync("SubmitNotification", "you have New submitted  orders", 1, NewsubmitNotificationModel.Id);
                    //var testMail = await Send("doaa.abdel.ext@cemex.com");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
                //_logger.LogError(e.ToString());

            }
            return RedirectToAction("Index");
        }

        public bool CheckStatisfiedQuantity(GeoFilterModel model)
        {
            bool result = false;
            int totalAssignedQuantity = (int)model.OrderModel.orders.Where(o => o.PriorityId == (int)CommanData.Priorities.Norm).Select(o => o.PriorityQuantity).Sum();//.Select(o=>o.Sum(o=>o.PriorityQuantity));//.ToList();//.Select(o=>o.pr)
            if (model.HoldModel.ReminingQuantity >= totalAssignedQuantity)
            {
                result = true;

            }
            else
            {
                float minusQuantity = totalAssignedQuantity - model.HoldModel.ReminingQuantity;
            }
            return result;
        }

        // GET: CSDeliveryOrderController/Details/5
        public ActionResult ShowSubmittedOrders()
        {
            try
            {
                GeoFilterModel geoFilterModel = StartData();
                return View(geoFilterModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }

        }

        public ActionResult ShowPickupSubmittedOrders()
        {
            try
            {
                GeoFilterModel geoFilterModel = StartData();
                return View(geoFilterModel);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ShowSubmittedOrders(GeoFilterModel Model)
        {
            try
            {
                List<OrderModel2> orderModels = new List<OrderModel2>();
                List<CustomerModel> customerModels = new List<CustomerModel>();
                DateTime selectedPriorityDate = Model.SelectedPriorityDate.Date;
                AspNetUser applicationUser = await _userManager.GetUserAsync(User);
                List<TerritoryModel> territoryModelSales = null;
                var roles = _userManager.GetRolesAsync(applicationUser).Result.ToList();
                if (roles.Contains("Sales"))
                {
                    territoryModelSales = _territoryService.GetTerritoryByUserId(applicationUser.Id);
                    Model.SalesTerritoriesSelectedIds = territoryModelSales.Select(t => t.Id).ToList();
                    Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModelSales.First().userId);
                    Model.Priorities = _priorityService.GetAllPrioritiesExceptExtra().Result.ToList();
                }
                else
                {
                    TerritoryModel territoryModel = _territoryService.GetTerritory(Model.TerritorySelectedId);
                    Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModel.userId);
                    Model.SubRegions = _regionService.GetAllISubRegions().Result;
                    Model.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -1, Name = "Select SubRegion" });
                    Model.States = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                    Model.States.Insert(0, new StateModel { Id = -1, Name = "Select State" });
                    Model.SubRegionSelectedId = -1;
                    Model.Territories = _territoryService.GetAllTerritoriesByStateId(Model.StateSelectedId).Result;
                    Model.Territories.Insert(0, new TerritoryModel { Id = -1, Name = "Select Territory" });
                }
                itemModels = _itemService.GetAllItems().Result;
                itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
                Model.Items = itemModels;
                Model.ItemSelectedId = -1;
            if(Model.HoldModel != null)
                {
                    Model.holdTotalAssignedQuantity = Model.HoldModel.QuotaQuantity - Model.HoldModel.ReminingQuantity;
                    if (Model.ZoneSelectedId > 0)
                    {

                        customerModels = _deliveryCustomerService.GetCutomersByZoneId(Model.ZoneSelectedId).Result;
                        Model.Zones = _zoneService.GetListOfZonesByTerritoryId(Model.TerritorySelectedId);
                        Model.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
                    }
                    else
                    {
                        List<ZoneModel> zoneModels = new List<ZoneModel>();
                        if (roles.Contains("Sales"))
                        {
                            zoneModels =  _zoneService.GetListOfZonesByTerritoryIds(Model.SalesTerritoriesSelectedIds).Result;

                        }
                        else
                        {
                            zoneModels = _zoneService.GetListOfZonesByTerritoryId(Model.TerritorySelectedId);

                        }
                        List<int> zoneIds = zoneModels.Select(z => z.Id).ToList();

                        customerModels = _deliveryCustomerService.GetCutomersByListOfZoneIds(zoneIds).Result;
                        Model.Zones = zoneModels;
                        Model.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
                    }

                    List<long> customerNumbers = customerModels.Select(c => c.Id).ToList();

                    orderModels = _orderService.GetSubmittedOdersByListOfCustomerNumbers(customerNumbers, selectedPriorityDate, selectedPriorityDate).Result.Where(o => o.OrderCategoryId == Model.orderType).ToList();

                    if (Model.ItemSelectedId != -1)
                    {
                        orderModels = orderModels.Where(o => o.ItemId == Model.ItemSelectedId).ToList();
                    }
                    Model.ordersQuantitySum = (float)orderModels.Sum(o => o.PriorityQuantity);
                    //TerritoryModel territoryModel = _territoryService.GetTerritory(Model.TerritorySelectedId);
                    //Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModel.userId);
                    Model.OrderModel = new OrderModel();
                    Model.OrderModel.orders = orderModels;
                    Model.Customers = customerModels;
                    //Model.SubRegions = _regionService.GetAllISubRegions().Result;
                    //Model.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -1, Name = "Select SubRegion" });
                    //Model.States = _stateService.GetStatesBySubRegionId(Model.SubRegionSelectedId).Result;
                    //Model.States.Insert(0, new StateModel { Id = -1, Name = "Select State" });
                    //Model.SubRegionSelectedId = -1;
                    //Model.Territories = _territoryService.GetAllTerritoriesByStateId(Model.StateSelectedId).Result;
                    //Model.Territories.Insert(0, new TerritoryModel { Id = -1, Name = "Select Territory" });

                }
                else
                {
                    ViewBag.Error = "There Is No Quota for This Priority Date";
                }

                if (Model.orderType == (int)CommanData.OrderCategory.Delivery)
                {
                    return View("ShowSubmittedOrders", Model);
                }
                else
                {
                    return View("ShowPickupSubmittedOrders", Model);

                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
                _logger.LogError(e.ToString());
            }
        }
        public ActionResult OrderCancel(long id)
        {
            try
            {
                OrderModel2 model = _orderService.GetOrder(id);
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCancel(long id, IFormCollection collection)
        {
            try
            {
                AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
                OrderModel2 model = _orderService.GetOrder(id);
                model.Status = "Cancelled";
                model.Submitted = true;
                model.SubmitTime = DateTime.Now;
                model.WHSubmittedID = applicationUser.Id;
                _orderService.UpdateOrder(model);
                TempData["message"] = "order has been cancelled";
                return RedirectToAction("ShowSubmittedOrders");

            }
            catch (Exception e)
            {
                //return RedirectToAction("ERROR404");
                TempData["message"] = "order not cancelled";
                return RedirectToAction("ShowSubmittedOrders");
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
            updateModel.Submitted = false;
            //updateModel.SubmitTime = null;
            bool updateOrderResult = _orderService.UpdateOrder(updateModel).Result;
            if (updateOrderResult == true)
            {

            }
            else
            {
            }
            return RedirectToAction("ShowSubmittedOrders");
        }
        public GeoFilterModel StartData()
        {
            try
            {
                GeoFilterModel geoFilterModel = new GeoFilterModel { };
                List<ItemModel> itemModels = new List<ItemModel>();
                AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
                List<string> roles = (List<string>)_userManager.GetRolesAsync(applicationUser).Result;

                var subRegionModels = _regionService.GetAllISubRegions().Result;
                subRegionModels.Insert(0, new SubRegionModel { Id = -1, Name = "select SubRegion" });
                geoFilterModel.SubRegions = subRegionModels;
                itemModels = _itemService.GetAllItems().Result;
                itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
                geoFilterModel.Items = itemModels;
                geoFilterModel.ItemSelectedId = -1;

                geoFilterModel.SubRegions = subRegionModels;
                geoFilterModel.SubRegionSelectedId = -1;

                geoFilterModel.SelectedPriorityDate = DateTime.Today;
                if(roles.Contains("Sales"))
                {
                    List<TerritoryModel> territoryModels = _territoryService.GetTerritoryByUserId(applicationUser.Id);
                    geoFilterModel.SalesTerritoriesSelectedIds = territoryModels.Select(t => t.Id).ToList();
                    geoFilterModel.Zones = _zoneService.GetListOfZonesByTerritoryIds(geoFilterModel.SalesTerritoriesSelectedIds).Result.ToList();
                    geoFilterModel.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });

                }
                return geoFilterModel;
            }
            catch (Exception e)
            {
                //return RedirectToAction("ERROR404");
                _logger.LogError(e.ToString());
            }
            return null;

        }
        public async Task<JsonResult> updateSeenNotification(long id)
        {
            try
            {
                bool updateResult = false;
                AspNetUser applicationUser = _userManager.GetUserAsync(User).Result;
                List<UserNotificationModel> userNotificationModels = new List<UserNotificationModel>();

                userNotificationModels = _userNotificationService.GetUserNotification(applicationUser.Id, id);
                userNotificationModels.ForEach(u => u.Seen = true);
                foreach (var userNotificationModel in userNotificationModels)
                {
                    updateResult = await _userNotificationService.UpdateUserNotification(userNotificationModel);

                }
                if (updateResult)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            return Json(false);

        }


        public async Task<IActionResult> Send(string toAddress)
        {
            try
            {
                var subject = "test mail";
                var body = "new order submit";
                await _emailSender.SendEmailAsync(toAddress, subject, body);
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("ERROR404");
            }

        }

        // GET: CSDeliveryOrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CSDeliveryOrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CSDeliveryOrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GeoFilterModel geoFilterModel)
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

        // GET: CSDeliveryOrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CSDeliveryOrderController/Edit/5
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

        // GET: CSDeliveryOrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CSDeliveryOrderController/Delete/5
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
