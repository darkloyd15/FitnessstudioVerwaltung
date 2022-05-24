using Abschlussprojekt_Fitnessstudio.EventModels;
using Abschlussprojekt_Fitnessstudio.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogInEvent>, IHandle<CustomerViewEvent>, IHandle<CustomerAddEvent>, IHandle<ChangeTrainingPlanEvent>
    {
        private readonly IEventAggregator _events;
        private readonly ISelectedCustomer _customer;

        public ShellViewModel(IEventAggregator events, ISelectedCustomer customer)
        {
            _events = events;
            _customer = customer;
            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<CustomerListViewModel>());
            _menuSwitch = true;
        }

       

        public string ViewTitle;
        


        private bool _menuSwitch;

        public bool MenuSwitch
        {
            get { return _menuSwitch; }
            set
            {
                _menuSwitch = value;
                NotifyOfPropertyChange(() => CanKunden);
                NotifyOfPropertyChange(() => CanTermine);
                NotifyOfPropertyChange(() => CanLogout);

            }
        }

        public bool CanKunden
        {
            get
            {
                return MenuSwitch;
            }
        }
        public bool CanTermine
        {
            get
            {
                return MenuSwitch;
            }
        }
        public bool CanLogout
        {
            get
            {
                return MenuSwitch;
            }
        }

        public void Logout()
        {
            ViewTitle = "Login";
            MenuSwitch = false;
            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }
        public void Kunden()
        {
            ViewTitle = "Benutzer-Listen-Ansicht";
            ActivateItemAsync(IoC.Get<CustomerListViewModel>());
        }
        public void Termine()
        {
            ViewTitle = "Termine";
            ActivateItemAsync(IoC.Get<ScheduleViewModel>());
        }

        public static void Help()
        {
            MessageBox.Show($"Kundenservice erreichbar unter: 01724795285 \n"
                + $" Erreichbar: Mon-Son von 12 bis 20 Uhr");
        }


        public Task HandleAsync(LogInEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = "Benutzer-Listen-Ansicht";
            MenuSwitch = true;
            return ActivateItemAsync(IoC.Get<CustomerListViewModel>());
        }

        public Task HandleAsync(CustomerViewEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = "Benutzer-Ansicht";
            return ActivateItemAsync(IoC.Get<CustomerViewModel>());
        }

        public Task HandleAsync(CustomerAddEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = "Benutzer-Hinzufügen";
            return ActivateItemAsync(IoC.Get<AddCustomerViewModel>());
        }

        public Task HandleAsync(ChangeTrainingPlanEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = $"{_customer.CurrentCustomer.FirstName} {_customer.CurrentCustomer.LastName} Trainingsplan";
            return ActivateItemAsync(IoC.Get<ChangeTrainigPlanViewModel>());
        }
    }
}
