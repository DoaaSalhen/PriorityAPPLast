using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using Microsoft.AspNetCore.Authorization;

namespace PriorityApp.Controllers.MasteData
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class StateController : Controller
    {
        private readonly IStateService _stateService;
        private readonly IRegionService _SubRegionService;

        public StateController(IStateService stateService, IRegionService SubRegionService)
        {
            _stateService = stateService;
            _SubRegionService = SubRegionService;
        }
        // GET: StateController
        public ActionResult Index()
        {
            Task<List<StateModel>> models= _stateService.GetAllStates();
            return View(models.Result);
        }

        // GET: StateController/Details/5
        public ActionResult Details(int id)
        {
            var model=_stateService.GetState(id);
            return View(model);
        }

        // GET: StateController/Create
        public ActionResult Create()
        {
            var subRegions = _SubRegionService.GetAllISubRegions();
            StateModel model = new StateModel
            {
                SubRegions = subRegions.Result,
                CreatedDate=DateTime.Now.Date,
                UpdatedDate = DateTime.Now.Date

            };
            return View(model);
        }

        // POST: StateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StateModel model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.IsVisible = true;
                model.IsDelted = false;
                var respone=_stateService.CreateState(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StateController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _stateService.GetState(id);
            model.SubRegions = _SubRegionService.GetAllISubRegions().Result;
            return View(model);

        }

        // POST: StateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StateModel model)
        {
            try
            {
                var respone = _stateService.CreateState(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StateController/Delete/5
        public ActionResult Delete(int id)
        {
            var model=_stateService.GetState(id);
            return View(model);
        }

        // POST: StateController/Delete/5
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
