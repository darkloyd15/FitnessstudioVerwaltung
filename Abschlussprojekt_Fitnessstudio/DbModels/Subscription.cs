using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Subscription
    {
        public Subscription()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? PricePerMonth { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
