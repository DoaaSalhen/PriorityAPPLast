using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriorityApp.Service.Implementation;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using Microsoft.AspNetCore.Authorization;

namespace PriorityApp.Controllers.MasteData
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class MainRegionController : Controller
    {
        private readonly IRegionService _IMainRegionService;

        public MainRegionController(IRegionService IMainRegionService)
        {
            _IMainRegionService = IMainRegionService;
        }

        // GET: MainRegionController
        public ActionResult Index()
        {
            Task <List<MainRegionModel>> MainRegions= _IMainRegionService.GetAllIMainRegions();
            return View(MainRegions.Result);
        }

        // GET: MainRegionController/Details/5
        public ActionResult Details(int id)
        {
            var model=_IMainRegionService.GetMainRegion(id);
            return View(model);
        }

        // GET: MainRegionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MainRegionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MainRegionModel model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.IsVisible = true;
                model.IsDelted = false;
                _IMainRegionService.CreateMainRegion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MainRegionController/Edit/5
        public ActionResult Edit(int id)
        {
            MainRegionModel model = _IMainRegionService.GetMainRegion(id);
            return View(model);
        }

        // POST: MainRegionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MainRegionModel model)
        {
            try
            {
                model.UpdatedDate = DateTime.Now;
                var respons=_IMainRegionService.UpdateMainRegion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MainRegionController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _IMainRegionService.GetMainRegion(id);
            return View(model);
        }

        // POST: MainRegionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                MainRegionModel model = _IMainRegionService.GetMainRegion(id);
                model.IsVisible = false;
                model.IsDelted = true;
                var response =_IMainRegionService.UpdateMainRegion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
