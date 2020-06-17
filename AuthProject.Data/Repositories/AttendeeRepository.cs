using AuthProject.Data.Entities;
using AuthProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthProject.Data.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly AuthPortalContext dbContext;

        public AttendeeRepository(AuthPortalContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> Add(AttendeeModel attendee)
        {
            var entity = Attendee.FromModel(attendee);
            dbContext.Attendees.Add(entity);
            return dbContext.SaveChangesAsync();
        }
    }
}
