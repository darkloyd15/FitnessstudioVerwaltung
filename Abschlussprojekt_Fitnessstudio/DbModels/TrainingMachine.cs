using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class TrainingMachine
    {
        public TrainingMachine()
        {
            TrainingMachinePlans = new HashSet<TrainingMachinePlan>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TrainingMachinePlan> TrainingMachinePlans { get; set; }
    }
}
