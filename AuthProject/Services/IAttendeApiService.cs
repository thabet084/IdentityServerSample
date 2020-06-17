﻿using AuthProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthProject.Services
{
    public interface IAttendeApiService
    {
        Task<AttendeeModel> AddAttendee(AttendeeModel attendee);
        Task<IEnumerable<ConferenceModel>> GetAllConferences();
        Task AddConference(ConferenceModel model);
    }
}