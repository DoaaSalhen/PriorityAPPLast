using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Auth;
using PriorityApp.Service.Models.MasterModels;
using Microsoft.AspNetCore.Authorization;

namespace PriorityApp.Controllers.MasteData
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TerritoryController : Controller
    {
        private readonly ITerritoryService _territoryService;
        private readonly IStateService _stateService;
        private readonly IUserService _userService;
        // GET: TerritoryController
        public TerritoryController(ITerritoryService territoryService, 
            IStateService stateService, 
            IUserService userService)
        {
            _territoryService = territoryService;
            _stateService = stateService;
            _userService = userService;
        }
        public async Task<ActionResult> Index()
        {
            var model=await _territoryService.GetAllTeritories();
            return View(model);
        }

        // GET: TerritoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TerritoryController/Create
        public ActionResult Create()
        {
            var stateModels = _stateService.GetAllStates();
            var userModels = _userService.GetAllUsers();
            TerritoryModel territoryModel = new TerritoryModel
            {
                userModels = userModels.Result,
                stateModels=stateModels.Result,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today
            };

            return View(territoryModel);
        }

        // POST: TerritoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TerritoryModel model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                model.IsVisible = true;
                model.IsDelted = false;
                var result = _territoryService.CreateTerritory(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TerritoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var territoryModel = _territoryService.GetTerritory(id);
            var stateModels = _stateService.GetAllStates();
            var userModels = _userService.GetAllUsers();
            territoryModel.userModels = userModels.Result;
            territoryModel.stateModels = stateModels.Result;
            
            return View(territoryModel);
        }

        // POST: TerritoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TerritoryModel model)
        {
            try
            {
                model.UpdatedDate = DateTime.Now;
                var result = _territoryService.CreateTerritory(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TerritoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TerritoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                TerritoryModel model = _territoryService.GetTerritory(id);
                model.IsVisible = false;
                model.IsDelted = true;
                var result =_territoryService.UpdateTerritory(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
