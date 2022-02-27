

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PriorityApp.Service.Models.MasterModels
{
    public class StateModel
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }
        public SubRegionModel SubRegion { get; set; }
        public List<SubRegionModel> SubRegions { get; set; }

        [Required]
        public string SubregionId { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
