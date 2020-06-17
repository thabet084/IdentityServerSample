using AuthProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthProject.Services
{
    public class AttendeApiService : IAttendeApiService
    {
        private readonly HttpClient client;

        public AttendeApiService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<AttendeeModel> AddAttendee(AttendeeModel attendee)
        {
            var response = await
                client.PostAsJsonAsync("/Attendee/Add", attendee);
            return await response.ReadContentAs<AttendeeModel>(); ;
        }

        public async Task<IEnumerable<ConferenceModel>> GetAllConferences()
        {
            var response = await client.GetAsync("/Conference");
            return await response.ReadContentAs<List<ConferenceModel>>();
        }

        public async Task AddConference(ConferenceModel model)
        {
            await client.PostAsJsonAsync("/Conference", model);
        }
    }
}
