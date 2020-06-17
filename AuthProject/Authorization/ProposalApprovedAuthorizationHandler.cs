using AuthProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.Web.Authorization
{
    /// <summary>
    /// Resource-based Policy
    /// its better as its centralized & reusable
    /// </summary>
    public class ProposalApprovedAuthorizationHandler : AuthorizationHandler<ProposalRequirement, ProposalModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProposalRequirement requirement, ProposalModel resource)
        {
            if (!resource.Approved)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
