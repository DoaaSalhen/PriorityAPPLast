using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers.MasteData
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SubRegionController : Controller
    {
        private readonly IRegionService _regionService;

        public SubRegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }
        // GET: SubRegionController
        public ActionResult Index()
        {
            var models= _regionService.GetAllISubRegions();
            return View(models.Result);
        }

        // GET: SubRegionController/Details/5
        public ActionResult Details(int id)
        {
            var model = _regionService.GetSubRegion(id);
            return View(model);
        }

        // GET: SubRegionController/Create
        public ActionResult Create()
        {
            Task<List<MainRegionModel>> MainRegionModels= _regionService.GetAllIMainRegions();
            var SubRegionModels = new SubRegionModel
            {
                mainRegions=MainRegionModels.Result,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today
             };
               
         
            return View(SubRegionModels);
        }

        // POST: SubRegionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubRegionModel model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.IsVisible = true;
                model.IsDelted = false;
                var response=_regionService.CreateSubRegion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubRegionController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _regionService.GetSubRegion(id);
            model.mainRegions = _regionService.GetAllIMainRegions().Result;
            return View(model);
        }

        // POST: SubRegionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubRegionModel model)
        {
            try
            {
                model.UpdatedDate = DateTime.Now;
                var response=_regionService.UpdateSubRegion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubRegionController/Delete/5
        public ActionResult Delete(int id)
        {
            var model=_regionService.GetSubRegion(id);
            return View(model);
        }

        // POST: SubRegionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SubRegionModel model = _regionService.GetSubRegion(id);
                model.IsVisible = false;
                model.IsDelted = true;
                var response=_regionService.UpdateSubRegion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
