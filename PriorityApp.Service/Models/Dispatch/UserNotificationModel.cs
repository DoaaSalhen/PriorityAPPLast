using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Service.Models.Dispatch
{
    public class UserNotificationModel
    {

        public string userId { get; set; }
        public long submitNotificationId { get; set; }

        public SubmitNotificationModel submitNotificationModel { get; set; }

        [Required]
        public bool Seen { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
