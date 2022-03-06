using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class AddPickUpOrderModel
    {

        public long OrderNumber { get; set; }

        public int OrderQuantity { get; set; }

        public int PriorityQuantity { get; set; }

        public int LineID { get; set; }

        public int PriorityId { get; set; }

        public List<PickUpOrder> pickUpOrders { get; set; }
        public List<long> selectedCustomers { get; set; }


        public List<MainRegionModel> MainRegions { get; set; }
        public List<SubRegionModel> SubRegions { get; set; }
        public List<StateModel> States { get; set; }
        public List<TerritoryModel> Territories { get; set; }
        public List<ZoneModel> Zones { get; set; }
        public List<CustomerModel> Customers { get; set; }

        public List<CustomerModel> TerritoryAllCustomers { get; set; }

        public List<ItemModel> Items { get; set; }
        public List<PriorityModel> Priorities { get; set; }

        public HoldModel HoldModel { get; set; }

            
        public long ItemSelectedId { get; set; }
        public DateTime SelectedPriorityDate { get; set; }
        public int MainRegionSelectedId { get; set; }
        public int SubRegionSelectedId { get; set; }
        public int StateSelectedId { get; set; }
        public int TerritorySelectedId { get; set; }
        public int ZoneSelectedId { get; set; }
        public long CustomerSelectedId { get; set; }
        public float holdTotalAssignedQuantity { get; set; }


    }
}
