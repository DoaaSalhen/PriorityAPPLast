using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class Warehouse:Entity<long>
    {
        public string Name { get; set; }
        public State State { get; set; }

        public int StateId { get; set; }
    }
}
