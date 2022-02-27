using PriorityApp.Models.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models.MasterModels
{
    public class ZoneModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        public int TerritoryId { get; set; }

        public TerritoryModel Territory { get; set; }
        public List<TerritoryModel> Territories { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }



    }
}
