using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;
using PriorityApp.Models.Models;

namespace PriorityApp.Service.Contracts.CustomerService
{
   public interface IPendService
    {
        public DataTable ReadExcelData(string filePath, string excelConnectionString);
        public bool WriteDataToSql(DataTable dt, string SqlConnectionString);
        public List<Order> GetPend();
        public Task<bool> ClearPend();

        public bool FixDuplication();

        public DataTable Preprocess(DataTable dt);

    }
}
