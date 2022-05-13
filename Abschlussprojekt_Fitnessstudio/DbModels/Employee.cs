﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Emailaddress { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public int? Address { get; set; }
        public string Password { get; set; }

        public virtual Address AddressNavigation { get; set; }
    }
}
