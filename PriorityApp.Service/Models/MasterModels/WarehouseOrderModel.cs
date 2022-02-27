using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
    public class WarehouseOrderModel
    {
        public DateTime SelectedPriorityDate { get; set; }
        public int SubRegionSelectedId { get; set; }
        public int StateSelectedId { get; set; }
        public int TerritorySelectedId { get; set; }
        public int ZoneSelectedId { get; set; }
        public int ItemSelectedId { get; set; }
        public List<SubRegionModel> SubRegions { get; set; }
        public List<StateModel> States { get; set; }
        public List<TerritoryModel> Territories { get; set; }
        public List<ItemModel> Items { get; set; }

        public List<WarehouseModel> WarehouseModels { get; set; }

        public List<WarehouseModel2> WarehouseModel2 { get; set; }
        public HoldModel HoldModel { get; set; }
        public int orderType { get; set; }
        public List<WarehouseOrderHoldModel> warehouseOrderHoldModels { get; set; }
        public List<PriorityModel> Priorities { get; set; }
        public string ViewCase { get; set; }
        public float holdTotalAssignedQuantity { get; set; }



    }
}
