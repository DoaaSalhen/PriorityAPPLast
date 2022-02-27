using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
    public class CustomerModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerType { get; set; }

        public List<StateModel> stateModels { get; set; }
        public int StateId { get; set; }

        public ZoneModel zone { get; set; }

        public List<ZoneModel> zoneModels { get; set; }
    

        [Required]
        public int ZoneId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
        public List<ItemModel> itemModels { get; set; }
        public string Comment { get; set; }
        public string Truck { get; set; }
        public int PrioritySelectedId { get; set; }



    }
}
