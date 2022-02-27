using PriorityApp.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PriorityApp.Service.Models.MasterModels
{
    public class TerritoryModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public StateModel state { get; set; }

        [Required]
        public int StateId { get; set; }

        public string userName { get; set; }

        [Required]
        public string userId { get; set; }

        [Required]
        public int Quota { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public UserModel user { get; set; }

        public List<UserModel> userModels { get; set; }
        public List<StateModel> stateModels { get; set; }

    }
}
