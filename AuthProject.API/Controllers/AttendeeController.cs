using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthProject.Data.Models;
using AuthProject.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendeeController : Controller
    {
        private readonly IAttendeeRepository repo;

        public AttendeeController(IAttendeeRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("{conferenceId}/{name}")]
        public IActionResult Post(int conferenceId, string name)
        {
            var attendee = repo.Add(
                new AttendeeModel { ConferenceId = conferenceId, Name = name });
            return StatusCode(201);
        }
    }
}