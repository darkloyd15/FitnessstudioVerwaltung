using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class TrainingMachinePlan
    {
        public int TraininMachineId { get; set; }
        public int TrainingPlanId { get; set; }
        public int? Iteration { get; set; }

        public virtual TrainingMachine TraininMachine { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
    }
}
