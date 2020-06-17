using AuthProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Data.Repositories
{
    public interface IProposalRepository
    {
        Task<int> Add(ProposalModel model);
        Task<ProposalModel> Approve(int proposalId);
        Task<List<ProposalModel>> GetAllForConference(int conferenceId);
    }
}
