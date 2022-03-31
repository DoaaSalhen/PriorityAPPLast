using @enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriorityApp.Service.Contracts;
using PriorityApp.Service.Contracts.Comman;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using PriporityApp_2.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Controllers
{
    public class ExportOrders : BaseController
    {
        private readonly IExcelService _excelService;

        private readonly IOrderService _orderService;
        private readonly IOrderCategoryService _orderCategoryService;
        private readonly IWarehouseOrderHoldService _warehouseOrderHoldService;
        private readonly ITerritoryService _territoryService;

        public ExportOrders(IExcelService excelService,
            IOrderService orderService,
            IOrderCategoryService orderCategoryService,
            IWarehouseOrderHoldService warehouseOrderHoldService,
            ITerritoryService territoryService)
        {
            _excelService = excelService;
            _orderService = orderService;
            _orderCategoryService = orderCategoryService;
            _warehouseOrderHoldService = warehouseOrderHoldService;
            _territoryService = territoryService;
        }

        // GET: ExportOrders
        public ActionResult Index()
        {
            try
            {
                ExportModel exportModel = new ExportModel();
                exportModel.SelectedPriorityDate = DateTime.Today;
                List<OrderCategoryModel> orderCategories = _orderCategoryService.GetAllOrderCategories().Result;
                orderCategories.Insert(0, new OrderCategoryModel { Id = 0, Name = "All" });
                exportModel.OrderCategoryModels = orderCategories;
                return View(exportModel);
            }
            catch(Exception e)
            {
                
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(ExportModel Model)
        {
            try
            {
                var orders = _orderService.GetSubmittedOdersByPriorityDate(Model.SelectedPriorityDate, Model.ToSelectedPriorityDate).Result;
                if(Model.OrderCategorySelectedId != 0)
                {
                    orders = orders.Where(o => o.OrderCategoryId == Model.OrderCategorySelectedId).ToList();
                    
                }
                for (int index =0; index< orders.Count; index++)
                {
                    if(orders[index].OrderCategoryId == (int)CommanData.OrderCategory.Warehouse)
                    {
                        orders[index].Customer.zone.Territory = _territoryService.GetTerritory(_warehouseOrderHoldService.GetWarehouseOrderHold(orders[index].Id).TerritoryId);

                    }
                }

                MemoryStream memoryStream = _excelService.ExportToExcel(orders);

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
            }
            catch
            {
                return RedirectToAction("ERROR404");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DispatchExport(GeoFilterModel model)
        {
            try
            {
                var orders = model.OrderModel.orders;

                MemoryStream memoryStream = _excelService.ExportToExcel(orders);

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportOrders.xlsx");
            }
            catch
            {
                return RedirectToAction("ERROR404");
            }
        }

        // GET: ExportOrders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExportOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExportOrders/Create
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

        // GET: ExportOrders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExportOrders/Edit/5
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

        // GET: ExportOrders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExportOrders/Delete/5
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
