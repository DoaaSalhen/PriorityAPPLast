using PriorityApp.Models.Auth;
using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class HoldModel
    {
        [Required]
        public DateTime PriorityDate { get; set; }
        [Required]
        public string userId { get; set; }

        public string UserName { get; set; }

        //public TerritoryModel territory { get; set; }

        //public int territoryId { get; set; }

        public float QuotaQuantity { get; set; }

        public float ReminingQuantity { get; set; }

        public float TempReminingQuantity { get; set; }


        public int Tolerance { get; set; }

        public float ExtraQuantity { get; set; }

        public List<HoldModel> holdModels { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<UserModel> userModels { get; set; }


    }
}
