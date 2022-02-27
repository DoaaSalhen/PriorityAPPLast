using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
    public class OrderCategoryModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
