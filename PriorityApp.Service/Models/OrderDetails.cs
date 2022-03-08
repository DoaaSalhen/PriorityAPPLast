using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class OrderDetails
    {
        public long Id { get; set; }
        public float PriorityQuantity { get; set; }
        public int PriorityId { get; set; }
        public string Comment { get; set; }
        public DateTime PriorityDate { get; set; }
        public string UserId { get; set; }
        public long ItemId { get; set; }
        public string Truck { get; set; }
    }
}
