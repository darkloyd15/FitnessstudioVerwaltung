using Abschlussprojekt_Fitnessstudio.DbModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.Models
{
    interface IAddedTrainingPlanModel
    {
        public BindableCollection<TrainingMachinePlan> trainingMachines { get; set; }
        public int TrainingPlanID { get; set; }

    }
}
