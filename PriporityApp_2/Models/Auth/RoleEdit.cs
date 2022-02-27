using PriorityApp.Models.Models.MasterModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriorityApp.Models.Auth
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AspNetUser> Members { get; set; }
        public IEnumerable<AspNetUser> NonMembers { get; set; }
    }
}
