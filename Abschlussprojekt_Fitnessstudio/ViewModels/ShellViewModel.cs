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
    public class ShellViewModel : Conductor<object>, IHandle<LogInEvent>, IHandle<CustomerViewEvent>, IHandle<CustomerAddEvent>, IHandle<ChangeTrainingPlanEvent>, IHandle<AddPlanToNewCustomerEvent>, IHandle<BackEventModel>, IHandle<ChangeNewTrainingPlanEventModel>
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

        public List<object> LastWindow { get; set; } = new();
        public object CurrentWindow { get; set; } = IoC.Get<CustomerListViewModel>();

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
            LastWindow.RemoveAll(x => true);
            CurrentWindow = IoC.Get<LoginViewModel>();
            ActivateItemAsync(CurrentWindow);
        }
        public void Kunden()
        {
            ViewTitle = "Benutzer-Listen-Ansicht";
            LastWindow.RemoveAll(x => true);
            CurrentWindow = IoC.Get<CustomerListViewModel>();
            ActivateItemAsync(CurrentWindow);
        }
        public void Termine()
        {
            ViewTitle = "Termine";
            LastWindow.Add(CurrentWindow);
            CurrentWindow= IoC.Get<ScheduleViewModel>();
            ActivateItemAsync(CurrentWindow);
        }

        public static void Help()
        {
            MessageBox.Show($"Kundenservice erreichbar unter: 01724795285 \n"
                + $" Erreichbar: Mon-Son von 12 bis 20 Uhr");
        }


        public Task HandleAsync(LogInEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = "Benutzer-Listen-Ansicht";
            LastWindow.Add(CurrentWindow);
            CurrentWindow = IoC.Get<CustomerListViewModel>();
            return ActivateItemAsync(CurrentWindow);
        }

        public Task HandleAsync(CustomerViewEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = "Benutzer-Ansicht";
            LastWindow.Add(CurrentWindow);
            CurrentWindow = IoC.Get<CustomerViewModel>();
            return ActivateItemAsync(CurrentWindow);
        }

        public Task HandleAsync(CustomerAddEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = "Benutzer-Hinzufügen";
            LastWindow.Add(CurrentWindow);
            CurrentWindow = IoC.Get<AddCustomerViewModel>();
            return ActivateItemAsync(CurrentWindow);
        }

        public Task HandleAsync(ChangeTrainingPlanEvent message, CancellationToken cancellationToken)
        {
            ViewTitle = $"{_customer.CurrentCustomer.FirstName} {_customer.CurrentCustomer.LastName} Trainingsplan";
            LastWindow.Add(CurrentWindow);
            CurrentWindow = IoC.Get<ChangeTrainigPlanViewModel>();
            return ActivateItemAsync(CurrentWindow);
        }

        public Task HandleAsync(AddPlanToNewCustomerEvent message, CancellationToken cancellationToken)
        {
            LastWindow.Add(CurrentWindow);
            CurrentWindow = IoC.Get<AddPlanToNewCustomerViewModel>();
            return ActivateItemAsync(CurrentWindow);
        }

        public Task HandleAsync(BackEventModel message, CancellationToken cancellationToken)
        {
            CurrentWindow = LastWindow.Last();
            LastWindow.Remove(LastWindow.Last());            
            return ActivateItemAsync(CurrentWindow);
        }

        public Task HandleAsync(ChangeNewTrainingPlanEventModel message, CancellationToken cancellationToken)
        {
            LastWindow.Remove(LastWindow.Last());
            CurrentWindow = IoC.Get<AddCustomerViewModel>();
            return ActivateItemAsync(CurrentWindow);
        }
    }
}
