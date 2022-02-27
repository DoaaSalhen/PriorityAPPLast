//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using PriorityApp.Models.Auth;
//using PriorityApp.Service.Models;
//using PriorityApp.Models.Models.MasterModels;
//using PriorityApp.Service.Contracts;
//using PriorityApp.Service.Models.MasterModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authorization;
//using PriporityApp_2.Controllers;

//namespace PriorityApp.Controllers.Sales
//{
//    [Authorize(Roles = "SuperAdmin, Admin, Sales")]
//    public class OrderController : BaseController
//    {

//        private readonly IOrderService _orderService;
//        private readonly ITerritoryService _territoryService;
//        private readonly IZoneService _zoneService;
//        private readonly IDeliveryCustomerService _deliveryCustomerService;
//        private readonly IHoldService _holdService;
//        private readonly IItemService _itemService;
//        private readonly UserManager<AspNetUser> _userManager;

//        public OrderController(IOrderService orderService,
//                                ITerritoryService territoryService,
//                                IZoneService zoneService,
//                                IDeliveryCustomerService deliveryCustomerService,
//                                IHoldService holdService,
//                                IItemService itemService,
//                               UserManager<AspNetUser> userManager
//)
//        {
//            _orderService = orderService;
//            _territoryService = territoryService;
//            _zoneService = zoneService;
//            _deliveryCustomerService = deliveryCustomerService;
//            _holdService = holdService;
//            _itemService = itemService;
//            _userManager = userManager;
//        }
//        public enum Priorities
//            {
//                No =2,
//                Norm =3,
//                Extra =4,
//                HPM =5

//            };
//        List<OrderModel2> orderModels = new List<OrderModel2>();
//        // GET: OrderController
//        public async Task<ActionResult> Index()
//        {
//            GeoFilterModel geoFilterModel = new GeoFilterModel();
//            List<ItemModel> itemModels = new List<ItemModel>();

//            AspNetUser applicationUser = await _userManager.GetUserAsync(User);

//            var territoryModel = _territoryService.GetTerritoryByUserId(applicationUser.Id);

//            List<ZoneModel> zoneModels = _zoneService.GetListOfZonesByTerritoryId(territoryModel.Id);
//            zoneModels.Insert(0, new ZoneModel { Id = -2, Name = "select zone" });
//            geoFilterModel.Zones = zoneModels;
//            itemModels = _itemService.GetAllItems().Result;
//            itemModels.Insert(0, new ItemModel { Id = -1, Name = "All" });
//            geoFilterModel.Items = itemModels;
//            geoFilterModel.ItemSelectedId = -1;

//            geoFilterModel.SelectedPriorityDate = DateTime.Today;

//            return View(geoFilterModel);
          
//        }

//        // GET: OrderController/Details/5
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: OrderController/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: OrderController/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: OrderController/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: OrderController/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: OrderController/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: OrderController/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        [HttpGet]



//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> SearchForOrders(GeoFilterModel Model)
//        {
//            try
//            {
//                AspNetUser applicationUser = await _userManager.GetUserAsync(User);
               
//                var territoryModel = _territoryService.GetTerritoryByUserId(applicationUser.Id);
//                List<OrderModel2> orderModels = new List<OrderModel2>();
//                List<CustomerModel> customerModels = new List<CustomerModel>();
//                DateTime selectedPriorityDate = Model.SelectedPriorityDate.Date;

//                if (Model.ZoneSelectedId != -1)
//                {

//                    customerModels = _deliveryCustomerService.GetCutomersByZoneId(Model.ZoneSelectedId).Result;
//                    Model.Zones = _zoneService.GetListOfZonesByTerritoryId(territoryModel.Id);
//                    Model.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
//                }
//                else
//                {
//                    List<ZoneModel> zoneModels = _zoneService.GetListOfZonesByTerritoryId(territoryModel.Id);
//                    List<int> zoneIds = zoneModels.Select(z => z.Id).ToList();

//                    customerModels = _deliveryCustomerService.GetCutomersByListOfZoneIds(zoneIds).Result;
//                    Model.Zones = zoneModels;
//                    Model.Zones.Insert(0, new ZoneModel { Id = -1, Name = "select Zone" });
//                }

//                List<long> customerNumbers = customerModels.Select(c => c.Id).ToList();
//                orderModels = _orderService.GetOdersByListOfCustomerNumbers(customerNumbers, selectedPriorityDate).Result.ToList();

//                if (Model.ItemSelectedId != -1)
//                {
//                    orderModels = orderModels.Where(o => o.ItemId == Model.ItemSelectedId).ToList();

//                }

//                Model.OrderModel = new OrderModel();
//                Model.OrderModel.orders = orderModels;
//                Model.Customers = customerModels;
//                Model.Customers.Insert(0, new CustomerModel { Id = -1, CustomerName = "select Customer" });


//                Model.HoldModel = _holdService.GetHold(Model.SelectedPriorityDate.Date, territoryModel.userId);
//                Model.OrderModel.holdModel = Model.HoldModel;
//                Model.Items = _itemService.GetAllItems().Result;
//                return View("index", Model);
//            }
//            catch
//            {
//                return RedirectToAction("ERROR404");
//            }
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> SaveOrders(GeoFilterModel Model)
//        {
//            AspNetUser applicationUser = await _userManager.GetUserAsync(User);
//            try
//            {
//                //bool result = CheckStatisfiedQuantity(Model);
//                bool updateOrderResult = false;
//                int SavedOrderCount = 0;
//                bool AllOrdersSaved = true;
//                //if (result == true)
//                //{
//                foreach (var orderModel in Model.OrderModel.orders.Where(o => o.PriorityId != null && o.Submitted == false))
//                {

//                    OrderModel2 updateModel = _orderService.GetOrder((long)orderModel.OrderNumber);
//                    updateModel.PriorityId = orderModel.PriorityId;
//                    updateModel.PriorityQuantity = orderModel.PriorityQuantity;
//                    updateModel.SavedBefore = true;
//                    updateModel.WHSavedID = applicationUser.Id;
//                    updateOrderResult = _orderService.UpdateOrder(updateModel).Result;
//                    if (updateOrderResult == true)
//                    {
//                        SavedOrderCount = SavedOrderCount + 1;
//                    }
//                    else
//                    {
//                        AllOrdersSaved = false;
//                    }


//                    //}
//                }
//                if (AllOrdersSaved)
//                {
//                    await _holdService.UpdateHold(Model.HoldModel);

//                }
//                TempData["SubmittedOrdersCount"] = SavedOrderCount;
//                return RedirectToAction("Index");
//            }
//            catch (Exception e)
//            {
//                return RedirectToAction("ERROR404");
//            }
//            //return RedirectToAction("Index");
//        }
//        public HoldModel CalculateRemainingQuota(OrderModel2 orderModel)
//        {
//            try
//            {
//                var holdModel = _holdService.GetHold(DateTime.Today.Date, orderModel.TerritoryId);
//                bool result = false;
//                if (holdModel != null)
//                {
//                    if (holdModel.ReminingQuantity > orderModel.PriorityQuantity)
//                    {
//                        float remainingQuota = holdModel.ReminingQuantity - (int)orderModel.PriorityQuantity;
//                        holdModel.ReminingQuantity = remainingQuota;
//                    }

//                }
//                else
//                {
//                    var territoryModel = _territoryService.GetTerritory((int)orderModel.TerritoryId);
//                    int remainingQuota = territoryModel.Quota - (int)orderModel.PriorityQuantity;

//                    holdModel = new HoldModel()
//                    {
//                        PriorityDate = DateTime.Today.Date,
//                        territoryId = (int)orderModel.TerritoryId,
//                        ReminingQuantity = remainingQuota

//                    };
//                    //result = _holdService.CreateHold(holdModel).Result;
//                }
//                return holdModel;
//            }
//            catch (Exception e)
//            {

//            }
//            return null;

//        }

//        public bool CheckStatisfiedQuantity(OrderModel model)
//        {
//            bool result = false;
//            var ordersPerPriorityDate = model.orders.Where(o => o.SubmitTime == null && o.PriorityId != (int)Priorities.No).GroupBy(o => o.PriorityDate).ToList();//.Select(o => o.Sum(o => o.PriorityQuantity));//.Select(o=>o.Sum(o=>o.PriorityQuantity));//.ToList();//.Select(o=>o.pr)
//            foreach (var order in ordersPerPriorityDate)
//            {
//                var PriorityDate = order.Key;
//                List<int> TerritoryId = order.Select(o => o.TerritoryId).Cast<int>().ToList();
//                var totalAssignedQuantity =(int) order.Sum(o => o.PriorityQuantity);
//                HoldModel holdModel = _holdService.GetHold(PriorityDate, TerritoryId[0]);

//                string message = "";
//                if (holdModel != null)
//                {
//                    float remainingQuantity = holdModel.ReminingQuantity - totalAssignedQuantity;
//                    if (holdModel.ReminingQuantity >= totalAssignedQuantity)
//                    {
//                        message = "Successful Request: you need to submit " + totalAssignedQuantity + " and your remaining quota =" + remainingQuantity;
//                        result = true;
//                    }
//                    else
//                    {
//                        message = "Failed Request:you need to submit " + totalAssignedQuantity + " and your remaining quota =" + remainingQuantity;
//                    }
//                }
//                else
//                {
//                    var territoryModel = _territoryService.GetTerritory((int)model.orders[0].TerritoryId);
//                    int remainingQuota = territoryModel.Quota - totalAssignedQuantity;
//                    if (territoryModel.Quota >= totalAssignedQuantity)
//                    {
//                        message = "Successful Request: you need to submit " + totalAssignedQuantity + " and your remaining quota =" + remainingQuota;
//                        result = true;
//                    }
                    

//                }
//            }
//            return result;
//        }
        
//        public  async Task<bool> SearchDeliveryOrders()
//        {
//            try
//            {
//                AspNetUser applicationUser = await _userManager.GetUserAsync(User);

//                var territoryModel = _territoryService.GetTerritoryByUserId(applicationUser.Id);
//                return true;
//            }
//            catch(Exception e)
//            {
//                return false;

//            }
//        }
//    }
//}