using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Models.MasterModels;
using PriorityApp.Service.Models.MasterModels.Search;
using Microsoft.AspNetCore.Authorization;

namespace PriorityApp.Controllers.MasteData
{
    //[Authorize]
    [Authorize(Roles ="SuperAdmin, Admin")]
    [ResponseCache(CacheProfileName = "Default30")]
    public class ItemController : Controller
    {
        private readonly IItemService _ItemService;
        
        public ItemController(IItemService ItemService)
        {
            _ItemService = ItemService;
        }
        // GET: ItemController
        public ActionResult Index()
        {
           Task<List<ItemModel>> items= _ItemService.GetAllItems();
            return View(items.Result);
        }

        // GET: ItemController/Details/5
        public ActionResult Details(int id)
        {
            var item = _ItemService.GetItem(id);
            return View(item);
        }

        // GET: ItemController/Create
        //[Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemModel Item)
        {
            try
            {
                Item.CreatedDate = DateTime.Now;
                Item.UpdatedDate = DateTime.Now;
                Item.IsVisible = true;
                Item.IsDelted = false;
                var response=_ItemService.CreateItem(Item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Edit/5
        public ActionResult Edit(int id)
        {
            var model=_ItemService.GetItem(id);
            return View(model);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemModel model)
        {
            try
            {

                model.UpdatedDate = DateTime.Now;
                var respons=_ItemService.UpdateItem(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _ItemService.GetItem(id);
            return View(model);
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var model = _ItemService.GetItem(id);
                model.IsVisible = false;
                model.IsDelted = true;
                var result = _ItemService.UpdateItem(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(ItemModel model)
        {
            string xx = model.SearchCreatedDate.Date.ToString("MM/dd/yyyy");
            if (model.SearchName == null && model.SearchCreatedDate.Date.ToString("mm/dd/yyyy")== "1/1/0001 12:00:00 AM" && model.SearchUpdatedDate.Date.ToString() == "1/1/0001 12:00:00 AM")
            {
                Task<List<ItemModel>> items = _ItemService.GetAllItems();
                model.itemModels = items.Result;
                return View(model);
            }
            else
            {
                Task<List<ItemModel>> items = _ItemService.GetItemByName(model);
                model.itemModels = items.Result;
                return View(model);
            }
            

            //ItemModel itemSearchModel = new ItemModel
            //{
            //    SearchName=model.SearchName,
            //    itemModels=items.Result
            //};

            //return View(itemSearchModel);
        }

        //public int preprocessDate(ItemModel model)
        //{
        //    if(model.SearchCreatedDate.ToString() == "1/1/0001")
        //    {
        //        model.SearchCreatedDate = null;
        //    }

        //    return 1;
        //}
    }
}
