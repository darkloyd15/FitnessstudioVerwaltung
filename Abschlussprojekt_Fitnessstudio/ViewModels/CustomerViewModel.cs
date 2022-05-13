using Abschlussprojekt_Fitnessstudio.DbModels;
using Abschlussprojekt_Fitnessstudio.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    public class CustomerViewModel : Screen
    {
        public Abschlussprojekt_FitnessstudioContext ctx = new();

        private readonly IEventAggregator _events;
        private readonly ISelectedCustomer _customer;

        public CustomerViewModel(IEventAggregator events, ISelectedCustomer customer)
        {
            _events = events;
            _customer = customer;
            CurrentCustomer = customer.CurrentCustomer;
            SubName = ctx.Subscriptions.FirstOrDefault(x => x.Id.Equals(CurrentCustomer.Subscription)).Name;
            TrainingPlan = new BindableCollection<TrainingMachinePlan>(ctx.TrainingMachinePlans.Where(x => x.TrainingPlanId.Equals(CurrentCustomer.TrainingPlanId)));
        }

        private Customer _currentCustomer;

        public Customer CurrentCustomer
        {
            get { return _currentCustomer; }
            set
            {
                _currentCustomer = value;
                NotifyOfPropertyChange(() => CurrentCustomer);
            }
        }
        private string _subName;

        public string SubName
        {
            get { return _subName; }
            set
            {
                _subName = value;
                NotifyOfPropertyChange(() => SubName);
            }
        }

        private BindableCollection<TrainingMachinePlan> _trainingPlan;

        public BindableCollection<TrainingMachinePlan> TrainingPlan
        {
            get { return _trainingPlan; }
            set
            {
                _trainingPlan = value;
                NotifyOfPropertyChange(() => TrainingPlan);
            }
        }








    }
}
