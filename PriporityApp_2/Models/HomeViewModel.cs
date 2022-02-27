using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Models
{
        public abstract class ViewModelBase
        {
            public string UserName { get; set; }
        }

        public class HomeViewModel : ViewModelBase
        {
        }
    
}
