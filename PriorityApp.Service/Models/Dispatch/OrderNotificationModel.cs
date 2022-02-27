using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models.Dispatch
{
    public class OrderNotificationModel
    {
        public OrderModel orderModel { get; set; }

        public long OrderId { get; set; }

        public SubmitNotificationModel submitNotificationModel { get; set; }

        public long submitNotificationId { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
