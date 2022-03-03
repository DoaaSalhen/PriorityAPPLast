using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class SubmittInfo
    {

        public List<OrderModel2> ordersTosubmit { get; set; }
        public List<SubmittedOrdersTerritories> submittedOrdersTerritories { get; set; }
        public TerritoryModel SubmittedOrdersTerritory  { get; set; }
        public List<HoldModel> holdModels { get; set; }
        public int OrdersCount { get; set; }

    }

    public class SubmittedOrdersTerritories
    {
        public TerritoryModel territorryModel { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
