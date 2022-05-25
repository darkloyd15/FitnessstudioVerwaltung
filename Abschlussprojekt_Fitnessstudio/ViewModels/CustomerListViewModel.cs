using Abschlussprojekt_Fitnessstudio.DbModels;
using Abschlussprojekt_Fitnessstudio.EventModels;
using Abschlussprojekt_Fitnessstudio.Models;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
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
            Content = new BindableCollection<Customer>(ctx.Customers.Include(c => c.SubscriptionNavigation));
            SelectedCustomer = Content.OrderBy(x => x.Id).First();
        }

        public void Search(string sender)
        {
            
            //WICHTI !!!!!! Var Name ÄNDERN!!!"!!!!!!!!!
            Customer lookforyou = Content.FirstOrDefault(x => x.FirstName.ToLower().StartsWith(sender.ToLower()));

            if (lookforyou != null)
            {
                SelectedCustomer = lookforyou;
            }
            
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
        public void AddNew()
        {           
            _events.PublishOnUIThreadAsync(new CustomerAddEvent());
        }

    }
}
