using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PriorityApp.Service.Contracts.CustomerService;
using PriporityApp_2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore.ImportExcel.Controllers
{
    [Authorize(Roles = "SuperAdmin , Admin , CustomerService, Sales")]
    public class PendController : Controller
    {
        private readonly ILogger<PendController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IPendService _pendService;
        public PendController(ILogger<PendController> logger,
            IWebHostEnvironment environment,
            IConfiguration configuration,
            IPendService pendService)
        {
            _logger = logger;
            _environment = environment;
            _configuration = configuration;
            _pendService = pendService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile postedFile, float QuantityToDelete)
        {

            if (_pendService.ClearPend().Result)
            {
                bool result = false;
                bool fixResult = false;
                string ExcelConnectionString = this._configuration.GetConnectionString("ExcelCon");
                string SqlConnectionString = this._configuration.GetConnectionString("SqlCon");
                DataTable dt = null;
                if (postedFile != null)
                {
                    //Create a Folder.
                    string path = Path.Combine(this._environment.WebRootPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Save the uploaded Excel file.
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string filePath = Path.Combine(path, fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }

                    dt = _pendService.ReadExcelData(filePath, ExcelConnectionString);
                    dt = _pendService.Preprocess(dt,QuantityToDelete);

                    if (dt.Rows.Count != 0)
                    {
                        result = _pendService.WriteDataToSql(dt, SqlConnectionString);
                    }
                    if (result == true)
                    {
                        fixResult = _pendService.FixDuplication();
                    }
                }
                int addedRows = 0;
                if (fixResult == true)
                {
                    foreach(DataRow row in dt.Rows)
                    {
                        addedRows = row.RowState != DataRowState.Deleted ? addedRows = addedRows+1 : addedRows;
                    }
                      ViewBag.Message = " File Uploaded Successfully && you have added "+addedRows + "new Orders";
                }
                else
                {
                    ViewBag.Error = " File Not Uploaded";
                }
                return View();
            }
            ViewBag.Error = "File Not Uploaded, Please Select Valid File";
            return View();
        }





















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
