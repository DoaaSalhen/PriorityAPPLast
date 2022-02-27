using PriorityApp.Models.Models.MasterModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PriorityApp.Models.Auth;
using PriorityApp.Service.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PriorityApp.Controllers.Auth
{
    [Authorize(Roles ="SuperAdmin , Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _RoleService;

        // GET: ItemController
        public AdminController(IUserService userService, IRoleService RoleService)
        {
            _RoleService = RoleService;
            _userService = userService;
        }

       

        // GET: AdminController
        public ActionResult Index()
        {
            Task<List<UserModel>> models = _userService.GetAllUsers();
            return View(models.Result);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public async Task<ActionResult> Create()
        {
            var models = _RoleService.GetAllRoles();
            UserModel model = new UserModel
            {
                roles=models.Result
            };
            return View(model);
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response= await _userService.CreateUser(model);
                  
                        return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: AdminController/Edit/5
        async public Task<ActionResult> Edit(string id)
        {
            var model=await _userService.GetUser(id);
           model.AsignedRolesNames = (string[])_userService.GetUserRoles(model).ToArray();
            var Rolemodels = _RoleService.GetAllRoles();
            model.roles = Rolemodels.Result;
            foreach (string name in model.AsignedRolesNames)
            {
                foreach(RoleModel role in model.roles)
                {
                    if(name ==role.Name)
                    {
                        role.isChacked = true;
                    }
                }

            }


            return View(model);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Edit(string id, UserModel model)
        {
            try
            {
                List<string> NewUserRolesNames = new List<string>();

                string[] OldUserRolesNames = (string[])_userService.GetUserRoles(model).ToArray();
                if(OldUserRolesNames.Count()!=0)
                {
                    foreach (string roleName in model.AsignedRolesNames)
                    {
                        foreach (string name in OldUserRolesNames)
                        {
                            if (name != roleName)
                            {
                                NewUserRolesNames.Add(roleName);
                            }
                        }
                    }
                    model.AsignedRolesNames = NewUserRolesNames.ToArray();
                }
               
                await _userService.UpdateUser(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                await _userService.DeleteUser(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
