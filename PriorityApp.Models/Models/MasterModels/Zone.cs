using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class Zone:Entity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int TerritoryId { get; set; }

        public Territory Territory { get; set; }

    }
}
