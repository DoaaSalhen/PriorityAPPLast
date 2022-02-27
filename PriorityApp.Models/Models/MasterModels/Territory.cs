using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class Territory:Entity<int>
    {
        [Required]
        public string Name { get; set; }
        public State State { get; set; }

        [Required]
        public int StateId { get; set; }
        [Required]
        public string userId { get; set; }
    }
}
