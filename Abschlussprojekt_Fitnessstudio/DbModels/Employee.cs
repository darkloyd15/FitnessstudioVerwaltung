using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Employee
    {
        public Employee()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emailaddress { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public string Password { get; set; }
        public int? Address { get; set; }

        public virtual Address AddressNavigation { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
