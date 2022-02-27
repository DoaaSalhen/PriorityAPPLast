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
    public class ZoneController : Controller
    {
        private readonly IZoneService _zoneService;
        private readonly ITerritoryService _territoryService;

        public ZoneController(IZoneService zoneService , ITerritoryService territoryService)
        {
            _zoneService = zoneService;
            _territoryService = territoryService;
        }
        // GET: ZoneController
        public async Task<ActionResult> Index()
        {
            var models = await _zoneService.GetAllZones();
            return View(models);
        }

        // GET: ZoneController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZoneController/Create
        async public Task<ActionResult> Create()
        {

            List<TerritoryModel> territorymodels = await _territoryService.GetAllTeritories();
            ZoneModel model = new ZoneModel
            {
                Territories = territorymodels,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today
            };
            return View(model);
        }

        // POST: ZoneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ZoneModel model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.IsVisible = true;
                model.IsDelted = false;
                var result = await _zoneService.CreateZone(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ZoneController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ZoneController/Edit/5
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

        // GET: ZoneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ZoneController/Delete/5
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
