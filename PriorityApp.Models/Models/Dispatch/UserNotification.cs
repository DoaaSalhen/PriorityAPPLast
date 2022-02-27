using PriorityApp.Models.Models.Entity;
using PriorityApp.Models.Models.MasterModels.Dispatch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PriorityApp.Models.Models.MasterModels
{
    public class UserNotification
    {
        [Key]
        public string userId { get; set; }
        [Key]
        public long submitNotificationId { get; set; }

        public SubmitNotification submitNotification { get; set; }

        [Required]
        public bool Seen { get; set; }

        public bool IsDelted { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


    }
}
