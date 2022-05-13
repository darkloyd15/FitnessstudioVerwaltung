using Abschlussprojekt_Fitnessstudio.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.Models
{
    public interface ISelectedCustomer
    {
        Customer CurrentCustomer { get; set; }
    }
}
