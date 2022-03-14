using PriorityApp.Models.Models.Entity;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class OrderModel2
    {
        public long Id { get; set; }
        public DateTime? PriorityDate { get; set; }
        //public State     State { get; set; }
        public int? StateId { get; set; }
        public string CustomerState { get; set; }
        public long CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public long? OrderNumber { get; set; }
        public string OrderDocument { get; set; }
        public int? LineID { get; set; }
        public DateTime OrderDate { get; set; }

        public long? ItemId { get; set; }

        public ItemModel Item { get; set; }
        //POD data
        public string PODNumber { get; set; }
        public string PODName { get; set; }
        public Zone      ZoneModel { get; set; }
        public int       ZoneId { get; set; }
        public string PODZoneName { get; set; }
        public string PODZoneState { get; set; }
        public string PODZoneAddress { get; set; }
        //
        public float? OrderQuantity { get; set; }
        public int?           PriorityId { get; set; }
        public PriorityModel? Priority { get; set; }

        public float?     PriorityQuantity { get; set; }

        public int?     OrginalQuantity { get; set; }

        public string   Status { get; set; }

        public TerritoryModel Territory { get; set; }

        public int?    TerritoryId { get; set; }

        public bool SavedBefore { get; set; }

        public bool Dispatched { get; set; }

        public DateTime? SubmitTime { get; set; }

        public int SubmitNumber { get; set; }
        public string Comment { get; set; }
        public bool? Submitted { get; set; }
        public int? SDMCU { get; set; }
        public string? WHSubmittedID { get; set; }
        public string? WHSavedID { get; set; }
        //public bool? delivery { get; set; }
        public string? Truck { get; set; }
        public OrderCategory OrderCategory { get; set; }
        public int? OrderCategoryId { get; set; }

        public bool? IsDelted { get; set; }

        public bool? IsVisible { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

    public  class OrderModel
    {
          public List<OrderModel2> orders { get; set; }
          public HoldModel holdModel { get; set; }
          public int Qouta { get; set; }
          public int SubmittedOrdersCount { get; set; }
        public int IsHoldHere { get; set; }


    }

}
