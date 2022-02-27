using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class Item:Entity<long>
    {

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public bool QutaCalc { get; set; }
        public bool HMP { get; set; }
        public long? OldItemNumber { get; set; }
        public string type { get; set; }
    }
}
