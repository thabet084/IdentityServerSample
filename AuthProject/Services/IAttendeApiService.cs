using AuthProject.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.Services
{
    public interface IAttendeApiService
    {

        Task<AttendeeModel> AddAttendee(AttendeeModel attendee);
        Task AddConference(ConferenceModel model);
        Task AddProposal(ProposalModel model);
        Task EditProposal(ProposalModel model);
        Task<ProposalModel> ApproveProposal(int proposalId);
        Task<ProposalModel> GetProposal(int proposalId);
        Task<IEnumerable<ConferenceModel>> GetAllConferences();
        Task<IEnumerable<ProposalModel>> GetAllProposalsForConference(int conferenceId);
    }
}
