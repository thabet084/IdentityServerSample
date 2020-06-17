using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.IdentityProvider.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CareerStarted { get; set; }
        public string FullName { get; set; }
    }
}
