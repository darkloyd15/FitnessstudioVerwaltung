using Abschlussprojekt_Fitnessstudio.DbModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.Models
{
    public class AddedCustomerModel : IAddedCustomerModel
    {
        public Customer NewCustomer { get; set; } = new();
        public Address NewCustomerAddress { get; set; } = new();
        public CustomerAddress CustomerAddress { get; set; } = new();
        public BindableCollection<TrainingMachinePlan> TrainingMachinePlan { get; set; } = new();
        public TrainingPlan TrainingPlan { get; set; } = new();
    }
}
