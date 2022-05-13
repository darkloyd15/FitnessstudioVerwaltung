using Abschlussprojekt_Fitnessstudio.DbModels;
using Abschlussprojekt_Fitnessstudio.EventModels;
using Abschlussprojekt_Fitnessstudio.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    class CustomerListViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private readonly ISelectedCustomer _customer;
        public Abschlussprojekt_FitnessstudioContext ctx = new();

        public CustomerListViewModel(IEventAggregator events, ISelectedCustomer customer)
        {
            _events = events;
            _customer = customer;
            Content = new BindableCollection<Customer>(ctx.Customers);
        }


        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                NotifyOfPropertyChange(() => SelectedCustomer);
            }
        }


        private BindableCollection<Customer> _content;

        public BindableCollection<Customer> Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }        

        public void Show()
        {
            _customer.CurrentCustomer = SelectedCustomer;
            _events.PublishOnUIThreadAsync(new CustomerViewEvent());
        }

    }
}
