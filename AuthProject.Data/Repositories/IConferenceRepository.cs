using AuthProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Data.Repositories
{
    public interface IConferenceRepository
    {
        Task<int> Add(ConferenceModel model);
        Task<List<ConferenceModel>> GetAll();
        Task<ConferenceModel> GetById(int id);
    }
}
