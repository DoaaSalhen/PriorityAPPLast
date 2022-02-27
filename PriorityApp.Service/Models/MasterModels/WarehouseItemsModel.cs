using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
   public class WarehouseItemsModel
    {
        public ItemModel ItemModel { get; set; }
        public float Quantity { get; set; }
        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
