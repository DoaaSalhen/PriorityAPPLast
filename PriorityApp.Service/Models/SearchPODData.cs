using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityApp.Service.Models
{
    public class SearchPODData
    {
        public long customerNumber { get; set; }
        public DateTime priorityDate { get; set; }

        public string PODNumber { get; set; }
        public string PODName { get; set; }
    }
}
