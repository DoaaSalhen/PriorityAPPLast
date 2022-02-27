using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class Customer:Entity<long>
    {

        [Required]
        [MaxLength(500)]
        public string CustomerName { get; set; }
        public string CutomerNameArabic { get; set; }

        public string CustomerType { get; set; }

        public Zone zone { get; set; }

        [Required]
        public int ZoneId { get; set; }
    }
}
