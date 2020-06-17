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
    public class ProposalRequirement : IAuthorizationRequirement
    {
    }
}
