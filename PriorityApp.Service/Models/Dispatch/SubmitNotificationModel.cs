using PriorityApp.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models.Dispatch
{
    public class SubmitNotificationModel
    {
        public long Id { get; set; }
        [Required]
        public string Message { get; set; }

        [Required]
        public int NumberOfSubmittedOrders { get; set; }

        public bool IsDelted { get; set; }

        public bool IsVisible { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool Seen { get; set; }

        public List<OrderNotificationModel> orderNotificationModel  { get; set;}

    }
}
