using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers.MasteData
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DeliveryCustomerController : Controller
    {
        private readonly IDeliveryCustomerService _deliveryCustomerService;
        private readonly IStateService _stateService;
        private readonly ITerritoryService _territoryService;
        private readonly IZoneService _zoneService;

        public DeliveryCustomerController(IDeliveryCustomerService deliveryCustomerService,
            IStateService stateService,
            ITerritoryService territoryService,
            IZoneService zoneService)
        {
            _deliveryCustomerService = deliveryCustomerService;
            _stateService = stateService;
            _territoryService = territoryService;
            _zoneService = zoneService;
        }

        // GET: DeliveryCustomerController
        public  ActionResult Index()
         {
            Task<List<CustomerModel>> models = _deliveryCustomerService.GetAllDeliveryCustomer();
            return View(models.Result);
        }

        // GET: DeliveryCustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryCustomerController/Create
        public ActionResult Create()
        {
            try
            {
                var models = _stateService.GetAllStates().Result;
                models.Insert(0, new StateModel { Id = -2, Name = "select State" });
                CustomerModel model = new CustomerModel
                {
                    stateModels = models,
                    CreatedDate = DateTime.Today,
                    UpdatedDate = DateTime.Today
                };
                return View(model);
            }
            catch(Exception e)
            {
                
            }
            return View();
        }

        // POST: DeliveryCustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerModel model)
        {
            bool result = false ;
            try
            {
                 result=await _deliveryCustomerService.CreateDeliveryCustomer(model);
                if (result == true)
                {
                    //return RedirectToAction("index");
                }
            }
            catch
            {
                //return View(result);
            }
            return RedirectToAction("index");
        }

        // GET: DeliveryCustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryCustomerController/Edit/5
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

        // GET: DeliveryCustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryCustomerController/Delete/5
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

        public async Task<JsonResult> DisplayZones(int id)
        {
            try
            {
                List<int> territoryIds = await _territoryService.GetTerritoryIdsByStateId(id);
                List<ZoneModel> zoneModels = await _zoneService.GetListOfZonesByTerritoryIds(territoryIds);
                zoneModels.Insert(0, new ZoneModel { Id = -2, Name = "select Zone" });
                return Json(new SelectList(zoneModels, "Id", "Name"));
            }
            catch(Exception e)
            {
                
            }
            return null;
        }
        }
}
