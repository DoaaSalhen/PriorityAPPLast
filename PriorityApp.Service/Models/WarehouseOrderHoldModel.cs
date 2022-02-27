using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class WarehouseOrderHoldModel
    {
        public OrderModel2 Order { get; set; }
        public long OrderId { get; set; }
        public long OrderId1 { get; set; }

        public HoldModel Hold { get; set; }

        public DateTime HoldPriorityDate { get; set; }
        public string HolduserId { get; set; }
        public List<WarehouseOrderHoldModel> WarehouseOrderHoldModels { get; set; }
    }
}
