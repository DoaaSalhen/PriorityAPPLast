using PriorityApp.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels.Dispatch
{
    public class SubmitNotification: EntityWithIdentityId<long>
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public int NumberOfSubmittedOrders { get; set; }


    }
}
