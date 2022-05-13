using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
