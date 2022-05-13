using Abschlussprojekt_Fitnessstudio.EventModels;
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
    public class ShellViewModel : Conductor<object>, IHandle<LogInEvent>, IHandle<CustomerViewEvent>
    {
        private readonly IEventAggregator _events;

        public ShellViewModel(IEventAggregator events)
        {            
            _events = events;
            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<CustomerListViewModel>());
            _menuSwitch = true;
        }

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
            MenuSwitch = false;
            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }
        public void Kunden()
        {            
            ActivateItemAsync(IoC.Get<CustomerListViewModel>());
        }
        public void Termine()
        {            
            ActivateItemAsync(IoC.Get<ScheduleViewModel>());
        }
        public void Help()
        {
            MessageBox.Show($"Kundenservice erreichbar unter: 01724795285 \n"
                + $" Erreichbar: Mon-Son von 12 bis 20 Uhr");
        }


        public Task HandleAsync(LogInEvent message, CancellationToken cancellationToken)
        {
            MenuSwitch = true;
            ActivateItemAsync(IoC.Get<CustomerListViewModel>());
            return Task.CompletedTask;
        }

        public Task HandleAsync(CustomerViewEvent message, CancellationToken cancellationToken)
        {
            return ActivateItemAsync(IoC.Get<CustomerViewModel>());
        }
    }
}
