using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class UploadPickUpModel
    {
        public List<SubRegionModel> SubRegions { get; set; }
        public List<StateModel> States { get; set; }
        public List<TerritoryModel> Territories { get; set; }

        public int SubRegionSelectedId { get; set; }
        public int StateSelectedId { get; set; }
        public int TerritorySelectedId { get; set; }
    }
}
