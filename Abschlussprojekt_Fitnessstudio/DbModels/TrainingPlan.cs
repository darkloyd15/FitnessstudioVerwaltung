using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class TrainingPlan
    {
        public TrainingPlan()
        {
            Customers = new HashSet<Customer>();
        }

        public int TrainingPlanId { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
