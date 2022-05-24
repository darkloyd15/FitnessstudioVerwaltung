using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Address
    {
        public Address()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Streetname { get; set; }
        public string City { get; set; }
        public int? Zipcode { get; set; }
        public string StreetNumber { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
