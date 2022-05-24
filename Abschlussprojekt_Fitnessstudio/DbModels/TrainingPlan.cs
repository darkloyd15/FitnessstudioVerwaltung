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
            TrainingMachinePlans = new HashSet<TrainingMachinePlan>();
        }

        public int TrainingPlanId { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<TrainingMachinePlan> TrainingMachinePlans { get; set; }
    }
}
