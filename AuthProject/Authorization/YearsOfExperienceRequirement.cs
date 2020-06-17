using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthProject.Web.Authorization
{
    /// <summary>
    /// AuthorizationRequirement used when there is special business authorization filter needed 
    /// e.g. if user years of expeience greater that specific value
    /// </summary>
    public class YearsOfExperienceRequirement : IAuthorizationRequirement
    {
        public int YearsOfExperienceRequired { get; set; }
        public YearsOfExperienceRequirement(int yearsOfExperienceRequired)
        {
            YearsOfExperienceRequired = yearsOfExperienceRequired;
        }
    }
}
