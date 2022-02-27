using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PriorityApp.Models.Auth;
using PriorityApp.Service.Contracts.Auth;
using PriporityApp_2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers.Auth
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        // GET: RoleController
        async public Task<ActionResult> Index()
        {
            var models=await _roleService.GetAllRoles();
            return View(models);
        }

        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            //if (id == 0) 
            //    return RedirectToAction("ERROR404");
            return View();
        }

        // GET: RoleController/Create
        public  ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleModel model)
        {
            try
            {
                var result=await _roleService.CreateRole(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var model=await _roleService.GetRole(id);
            return View(model);
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, RoleModel model)
        {
            try
            {
                var response=await _roleService.UpdateRole(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, RoleModel model)
        {
            try
            {
                var result = await _roleService.DeleteRole(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
