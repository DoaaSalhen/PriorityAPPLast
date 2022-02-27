using PriorityApp.Models.Models.MasterModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PriorityApp.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers.Auth
{
    public class RController2 : Controller
    {
    //    private RoleManager<IdentityRole> roleManager;
    //    private UserManager<AspNetUser> userManager;

    //    public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<AspNetUser> userMrg)
    //    {
    //        roleManager = roleMgr;
    //        userManager = userMrg;
    //    }
    //    // GET: RoleController
    //    public ActionResult Index()
    //    {
    //        return View(roleManager.Roles);
    //    }
    //    private void Errors(IdentityResult result)
    //    {
    //        foreach (IdentityError error in result.Errors)
    //            ModelState.AddModelError("", error.Description);
    //    }
    //    // GET: RoleController/Details/5
    //    public ActionResult Details(int id)
    //    {
    //        return View();
    //    }

    //    // GET: RoleController/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: RoleController/Create
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> CreateAsync([Required] string name) 
    //    {
    //        try
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
    //                if (result.Succeeded)
    //                    return RedirectToAction("Index");
    //                else
    //                    Errors(result);
    //            }
    //            return View(name);
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: RoleController/Edit/5
    //    public async Task<ActionResult> Edit(string id)
    //    {
    //            IdentityRole role = await roleManager.FindByIdAsync(id);
    //            List<AspNetUser> members = new List<AspNetUser>();
    //            List<AspNetUser> nonMembers = new List<AspNetUser>();
    //            List<AspNetUser> users = new List<AspNetUser>();
    //            users= userManager.Users.ToList() ;
    //            foreach (AspNetUser user in users)
    //            {
    //                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
    //                list.Add(user);
    //            }
    //            return View(new RoleEdit
    //            {
    //                Role = role,
    //                Members = members,
    //                NonMembers = nonMembers
    //            });
            
    //    }

    //    // POST: RoleController/Edit/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Edit(RoleModification model)
    //    {
    //        try
    //        {
    //            IdentityResult result;
    //            if (ModelState.IsValid)
    //            {
    //                foreach (string userId in model.AddIds ?? new string[] { })
    //                {
    //                    AspNetUser user = await userManager.FindByIdAsync(userId);
    //                    if (user != null)
    //                    {
    //                        result = await userManager.AddToRoleAsync(user, model.RoleName);
    //                        if (!result.Succeeded)
    //                            Errors(result);
    //                    }
    //                }
    //                foreach (string userId in model.DeleteIds ?? new string[] { })
    //                {
    //                    AspNetUser user = await userManager.FindByIdAsync(userId);
    //                    if (user != null)
    //                    {
    //                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
    //                        if (!result.Succeeded)
    //                            Errors(result);
    //                    }
    //                }
    //            }

    //            if (ModelState.IsValid)
    //                return RedirectToAction(nameof(Index));
    //            else
    //                return await Edit(model.RoleId);
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: RoleController/Delete/5
    //    public ActionResult Delete(int id)
    //    {
    //        return View();
    //    }

    //    // POST: RoleController/Delete/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> DeleteAsync(string id)
    //    {
    //        try
    //        {
    //            IdentityRole role = await roleManager.FindByIdAsync(id);
    //            if (role != null)
    //            {
    //                IdentityResult result = await roleManager.DeleteAsync(role);
    //                if (result.Succeeded)
    //                    return RedirectToAction("Index");
    //                else
    //                    Errors(result);
    //            }
    //            else
    //                ModelState.AddModelError("", "No role found");
    //            return View("Index", roleManager.Roles);
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    }
}
