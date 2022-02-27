using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Models;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace PriorityApp.Service.Contracts.Comman
{
    public interface IExcelService
    {
        public DataTable ReadExcelData(string filePath, string excelConnectionString);

        public MemoryStream ExportToExcel(List<OrderModel2> models);

        public MemoryStream WritePickUpTemplateToExcel(List<ItemModel> models, List<CustomerModel> customerModels);
        public MemoryStream WritQuotaTemplateToExcel(List<AspNetUser> salesUsers);
    }
}
