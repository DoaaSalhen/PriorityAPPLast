using Microsoft.EntityFrameworkCore;
using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class SubRegion:LookupEntity
    {

        public  MainRegion MainRegion { get; set; }
        public string RegionCode { get; set; }
        public int MainRegionId { get; set; }
    }
}
