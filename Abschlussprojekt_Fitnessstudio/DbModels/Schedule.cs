using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Schedule
    {
        public string EmployeeId { get; set; }
        public string CustomerId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
