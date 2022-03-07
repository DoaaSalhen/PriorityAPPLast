using PriorityApp.Models.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PriorityApp.Models.Models
{
    public class WarehouseOrderHold
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OrderId { get; set; }

        public Order Order { get; set; }

        public Hold Hold { get; set; }

        public DateTime HoldPriorityDate { get; set; }
        public string HolduserId { get; set; }
        //public Territory Territory { get; set; }
        public int TerritoryId { get; set; }



    }
}
