using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class OrderCategory: EntityWithIdentityId<int>
    {
        public string Name { get; set; }
    }
}
