using Abschlussprojekt_Fitnessstudio.DbModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.Models
{
    public interface IAddedCustomerModel
    {
        public Customer NewCustomer{ get; set; }
        public Address NewCustomerAddress { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public BindableCollection<TrainingMachinePlan> TrainingMachinePlan { get; set; }
        public TrainingPlan TrainingPlan { get; set; }

    }
}
