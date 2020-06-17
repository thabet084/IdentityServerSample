using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.Web.Authorization
{
    /// <summary>
    /// AuthorizationRequirement & AuthorizationHandler  used when there is special business authorization filter needed 
    /// e.g. if user years of expeience greater that specific value
    /// </summary>
    public class YearsOfExperienceAuthorizationHandler : AuthorizationHandler<YearsOfExperienceRequirement>
    {
      

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, YearsOfExperienceRequirement requirement)
        {
            if(!context.User.HasClaim(c=>c.Type=="CareerStarted" && c.Issuer== "https://localhost:44319/"))
            {
                return Task.CompletedTask;
            }

            var careerStarted = DateTimeOffset.Parse(context.User.FindFirst(c => c.Type == "CareerStarted" && c.Issuer == "https://localhost:44319/").Value);

            var yearsOfExperience = Math.Round((DateTimeOffset.Now - careerStarted).TotalDays / 365);

            if (yearsOfExperience >= requirement.YearsOfExperienceRequired)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
