using AuthProject.Data.Models;
using AuthProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConferenceController : Controller
    {
        private readonly IConferenceRepository repo;

        public ConferenceController(IConferenceRepository repo)
        {

            this.repo = repo;
        }

        public async Task<IEnumerable<ConferenceModel>> GetAll()
        {
            return await repo.GetAll();
        }

        [HttpPost]
        public void Add(ConferenceModel conference)
        {
            repo.Add(conference);
        }
    }
}
