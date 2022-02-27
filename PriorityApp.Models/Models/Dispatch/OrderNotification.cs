using PriorityApp.Models.Models.Entity;
using PriorityApp.Models.Models.MasterModels.Dispatch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.Dispatch
{
    public class OrderNotification : EntityWithIdentityId<long>
    {
        public Order order { get; set; }

        public long OrderId { get; set; }

        public SubmitNotification submitNotification { get; set; }

        public long submitNotificationId { get; set; }

    }
}
