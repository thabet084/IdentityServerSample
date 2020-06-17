using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Data.Models;
using AuthProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProposalController
    {
        private readonly IProposalRepository repo;

        public ProposalController(IProposalRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet("GetAll/{conferenceId}")]
        public async Task<IEnumerable<ProposalModel>> GetAll(int conferenceId)
        {
            return await repo.GetAllForConference(conferenceId);
        }

        [HttpPost("Add")]
        public void Add([FromBody]ProposalModel model)
        {
            repo.Add(model);
        }

        [HttpGet("Approve/{proposalId}")]
        public async Task<ProposalModel> Approve(int proposalId)
        {
            return await repo.Approve(proposalId);
        }

        [HttpPost("Edit")]
        public async Task<int> Edit([FromBody]ProposalModel model)
        {
            return await  repo.Edit(model);
        }

        [HttpGet("Get/{proposalId}")]
        public async Task<ProposalModel> Get(int proposalId)
        {
            return await repo.Get(proposalId);
        }
    }
}
