using AuthProject.Data.Entities;
using AuthProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Data.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        private readonly AuthPortalContext dbContext;

        public ProposalRepository(AuthPortalContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<ProposalModel>> GetAllForConference(int conferenceId)
        {
            return dbContext.Proposals.Where(p => p.ConferenceId == conferenceId).Select(p => p.ToModel()).ToListAsync();
        }


        public Task<int> Add(ProposalModel model)
        {
            var entity = Proposal.FromModel(model);
            dbContext.Proposals.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public async Task<ProposalModel> Approve(int proposalId)
        {
            var proposal = await dbContext.Proposals.FirstAsync(p => p.Id == proposalId);
            proposal.Approved = true;
            await dbContext.SaveChangesAsync();
            return proposal.ToModel();
        }
    }
}
