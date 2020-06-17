using System;
using System.Collections.Generic;
using System.Text;

namespace AuthProject.Data.Models
{
    public class AttendeeModel
    {
        public int Id { get; set; }
        public int ConferenceId { get; set; }
        public string Name { get; set; }
    }
}
