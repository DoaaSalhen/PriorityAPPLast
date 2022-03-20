using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class NewDataModel
    {
        public int newDataId { get; set; }
        public List<ZoneModel> Zones { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<ItemModel> Items { get; set; }
        public List<StateModel> States { get; set; }

    }
}
