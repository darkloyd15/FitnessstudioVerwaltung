using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class CustomerAddress
    {
        public int? AddressId { get; set; }
        public string CustomerId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
