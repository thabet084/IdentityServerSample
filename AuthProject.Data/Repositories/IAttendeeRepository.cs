using AuthProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Data.Repositories
{
    public interface IAttendeeRepository
    {
        Task<int> Add(AttendeeModel attendee);
    }
}
