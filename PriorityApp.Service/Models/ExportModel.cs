using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class ExportModel
    {

        public DateTime SelectedPriorityDate { get; set; }

        public List<OrderCategoryModel> OrderCategoryModels  { get; set; }

        public int OrderCategorySelectedId { get; set; }
        public DateTime ToSelectedPriorityDate { get; set; }


    }
}
