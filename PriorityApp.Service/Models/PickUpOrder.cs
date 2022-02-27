using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class PickUpOrder
    {
        public CustomerModel Customer { get; set; }
        public long    OrderNumber { get; set; }

        public int     OrderQuantity { get; set; }

        public int     PriorityQuantity { get; set; }

        public int     LineID { get; set; }

        public int     PriorityId { get; set; }
        public long    ItemSelectedId { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
