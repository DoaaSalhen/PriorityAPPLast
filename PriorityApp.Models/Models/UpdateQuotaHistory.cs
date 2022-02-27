using PriorityApp.Models.Models.Entity;
using PriorityApp.Models.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models
{
    public class UpdateQuotaHistory:Entity<int>
    {
        [Required]
        public int  OldQuantity{ get; set; }
        [Required]
        public int CurrentQuantity { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }

        public Territory territory { get; set; }
        public DateTime PriorityDate { get; set; }
    }
}
