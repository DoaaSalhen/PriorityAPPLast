
using PriorityApp.Models.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PriorityApp.Service.Models.MasterModels
{
    public class SubRegionModel
    {
        public int Id { get; set; }

        [MaxLength(3)]
        public string RegionCode { get; set; }
        [Required] 
        public string Name { get; set; }
        public List<MainRegionModel> mainRegions { get; set; }
        public  MainRegionModel mainRegion { get; set; }
        [Required]
        public int mainRegionId { get; set; }
        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
