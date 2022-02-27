using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriorityApp.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriorityApp.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriorityApp.Service.Models.MasterModels;

namespace PriorityApp.Controllers.CSales
{
    public class FilterController : Controller
    {
        private readonly IRegionService _regionService;
        private readonly IStateService _stateService;
        GeoFilterModel geoFilterModel = new GeoFilterModel{};

        public FilterController(IRegionService regionService, IStateService stateService)
        {
            _regionService = regionService;
            _stateService = stateService;
        }
        // GET: FilterController
        public ActionResult Index()
        {
            //var mainRegionmodels=_regionService.GetAllIMainRegions();
            //GeoFilterModel geoFilterModel = new GeoFilterModel
            //{
            //    MainRegions=mainRegionmodels.Result
            //};
            //return View(geoFilterModel);
            return View();
        }

        public ActionResult MainRegionFilter()
        {
            var mainRegionmodels = _regionService.GetAllIMainRegions();
            geoFilterModel.MainRegions = mainRegionmodels.Result;

            geoFilterModel.MainRegions.Insert(0, new Service.Models.MasterModels.MainRegionModel { Id =-2, Name="select Main Region"});
            geoFilterModel.MainRegions.Insert(1, new Service.Models.MasterModels.MainRegionModel { Id = -1, Name = "All" });

            return View("GeoFilter", geoFilterModel);
        }

        [HttpGet]
        public JsonResult SubRegionFilter(int id)
        {
            var subRegionModels = new List<SubRegionModel>();
            if (id == -1)
            {
                 subRegionModels = _regionService.GetAllISubRegions().Result;
            }
            else
            {
                 subRegionModels = _regionService.GetSubRegionByMainRegionId(id).Result;
            }
        
            // geoFilterModel.SubRegions = sunRegionmodels.Result;
            //geoFilterModel.SubRegions.Insert(0, new Service.Models.MasterModels.SubRegionModel { Id = -2, RegionName = "select Sub Region" });
            subRegionModels.Insert(0, new SubRegionModel { Id = -2, Name = "select Sub Region" });
            subRegionModels.Insert(1, new SubRegionModel { Id = -1, Name = "All" });
            return Json(new SelectList(subRegionModels, "Id", "Name"));
        }

        public JsonResult StateFilter(int id)
        {
            var stateModels = new List<StateModel>();
            if (id ==  -1)
            {
                stateModels = _stateService.GetAllStates().Result;
            }
            else
            {
                stateModels = _stateService.GetStatesBySubRegionId(id).Result;
            }
            stateModels.Insert(0, new StateModel { Id = -2, Name = "select state" });
            stateModels.Insert(1, new StateModel { Id = -1, Name = "All" });
            return Json(new SelectList(stateModels, "Id", "Name"));
        }
        // GET: FilterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilterController/Create
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

        // GET: FilterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilterController/Edit/5
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

        // GET: FilterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilterController/Delete/5
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
