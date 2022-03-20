using @enum;
using PriorityApp.Models.Models.MasterModels.Dispatch;
using PriorityApp.Service.Models.Dispatch;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class GeoFilterModel
    {
        public List<MainRegionModel> MainRegions { get; set; }
        public List<SubRegionModel> SubRegions { get; set; }
        public List<StateModel> States { get; set; }
        public List<TerritoryModel> Territories { get; set; }
        public List<ZoneModel> Zones { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<ItemModel> Items { get; set; }
        public List<PriorityModel> Priorities { get; set; }
        public OrderModel OrderModel { get; set; }
        public HoldModel HoldModel { get; set; }

        public long ItemSelectedId { get; set; }
        public DateTime SelectedPriorityDate { get; set; }
        public int MainRegionSelectedId { get; set; }
        public int SubRegionSelectedId { get; set; }
        public int StateSelectedId { get; set; }
        public int TerritorySelectedId { get; set; }
        public List<int> SalesTerritoriesSelectedIds { get; set; }
        public int ZoneSelectedId { get; set; }
        public long CustomerSelectedId { get; set; }
        //public string holdUserId { get; set; }
        public List<DispatchCaseModel> DispatchCases { get; set; }
        public int DispatchCaseSelectedId { get; set; }
        
        public TerritoryModel SalesmanTerritory { get; set; }

        public List<UserNotificationModel> userNotificationModels { get; set; }

        public int orderType { get; set; }
        public List<WarehouseModel> warehouseModels { get; set; }
        public long LastSubmitNotificationId { get; set; }
        public float ordersQuantitySum { get; set; }
        public string viewCase { get; set; }
        public float holdTotalAssignedQuantity { get; set; }
        public DateTime ToSelectedPriorityDate { get; set; }
        public int PendingSelectedId { get; set; }
        public bool SavedOnly { get; set; }
        public bool PendingOnly { get; set; }


        public List<SortingModel> FilterColoumns;



    }
}
