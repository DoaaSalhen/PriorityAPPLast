using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class State:LookupEntity
    {
        public int SubregionId { get; set; }
        public SubRegion Subregion { get; set; }


        ////public Region DistRegion { get; set; }
    }
}
