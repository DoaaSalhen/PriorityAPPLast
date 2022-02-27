using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PriorityApp.Models.Models.MasterModels
{
    // Add profile data for application users by adding properties to the authtestUser class
    public class AspNetUser : IdentityUser
    {

        public string FirstName  { get; set; }
        public String LastName { get; set; }
       // public DateTime CreatedDate { get; set; }
       // public DateTime UpdatedDate { get; set; }

    }
}
