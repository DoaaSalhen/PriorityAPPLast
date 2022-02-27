using PriorityApp.Models.Models.Entity;
using PriorityApp.Models.Models.MasterModels;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models
{
    class UpdateQuotaHistory:Entity<int>
    {
        [Required]
        public int  OldQuantity{ get; set; }
        [Required]
        public int CurrentQuantity { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        public TerritoryModel territory { get; set; }
    }
}
