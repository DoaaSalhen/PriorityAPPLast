using PriorityApp.Models.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models
{
    public class Hold
    {
        [Key]
        public DateTime  PriorityDate{ get; set; }
        [Key]
        public string userId { get; set; }
        //public Territory territory { get; set; }
        //public int     territoryId { get; set; }
        public float     QuotaQuantity { get; set; }

        public float     ReminingQuantity { get; set; }

        public float TempReminingQuantity { get; set; }

        public int     Tolerance { get; set; }
        public float   ExtraQuantity { get; set; }

    }
}
