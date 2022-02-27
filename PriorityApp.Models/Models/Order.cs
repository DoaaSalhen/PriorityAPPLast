using Microsoft.EntityFrameworkCore;
using PriorityApp.Models.Models.Entity;
using PriorityApp.Models.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PriorityApp.Models.Models
{
    [Index(nameof(OrderDate), Name = "Order_OrderDate_idx")]
    [Index(nameof(ItemId), Name = "Order_item_idx")]
    [Index(nameof(CustomerId), Name = "Order_CustomerId_idx")]
    [Index(nameof(OrderDate), Name = "Order_OrderDate_idx")]
    [Index(nameof(OrderDate), Name = "Order_OrderDate_idx")]
    [Index(nameof(PriorityId), Name = "Order_PriorityId_idx")]
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public long OrderNumber { get; set; }
        public DateTime  PriorityDate { get; set; }
        public Customer   Customer   { get; set; }
        public long CustomerId { get; set; }
        public int? CustomerType { get; set; }

        public string    OrderDocument { get; set; }
        public int?       LineID { get; set; }
        public DateTime?  OrderDate { get; set; }
        
        public long       ItemId { get; set; }
        public Item      Item { get; set; }
        //POD data
        public long?    PODNumber { get; set; }
        public string    PODName { get; set; }
        public string    PODZoneName { get; set; }
        public string    PODZoneState { get; set; }
        public string    PODZoneAddress { get; set; }
        //
        public float?       OrderQuantity { get; set; }
        public int       PriorityId { get; set; }

        public Priority  Priority { get; set; }

        public float?       PriorityQuantity { get; set; }

        public int?       OrginalQuantity { get; set; }

        public string    Status { get; set; }

        public int?  SubmitNumber { get; set; }

        public bool?     SavedBefore { get; set; }
        public bool?      Submitted { get; set; }

        public DateTime?  SubmitTime { get; set; }

        public bool? Dispatched { get; set; }

        public int? SDMCU { get; set; }
        public string? WHSubmittedID { get; set; }
        public string? WHSavedID { get; set; }

        public string?    Comment { get; set; }
        //public bool delivery { get; set; }
        public string? Truck { get; set; }
        public OrderCategory OrderCategory { get; set; }
        public int? OrderCategoryId { get; set; }



    }
}
