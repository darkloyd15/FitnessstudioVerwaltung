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

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    class AddCustomerViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private readonly IAddedCustomerModel _addedCustomer;
        public Abschlussprojekt_FitnessstudioContext ctx = new();

        public AddCustomerViewModel(IEventAggregator events, IAddedCustomerModel addedCustomer)
        {
            _events = events;
            _addedCustomer = addedCustomer;

            Abo = new();
            ctx.Subscriptions.ToList().ForEach(x => Abo.Add(x.Name));
            if (_addedCustomer.NewCustomer != null)
            {
                _addedCustomer.NewCustomer = _addedCustomer.NewCustomer;
            }
            if (_addedCustomer.NewCustomerAddress != null)
            {
                _addedCustomer.NewCustomerAddress = _addedCustomer.NewCustomerAddress;
            }
            TrainingPlan = new();
            if (_addedCustomer.TrainingMachinePlan != null)
            {
                TrainingPlan = _addedCustomer.TrainingMachinePlan;
            }

            if (_addedCustomer.NewCustomer.Birthday == null)
            {
                _addedCustomer.NewCustomer.Birthday = new DateTime(1999, 01, 01);
            }
            if (_addedCustomer.NewCustomer.Subscription == null)
            {
                _addedCustomer.NewCustomer.Subscription = 1;
            }


            Firstname = _addedCustomer.NewCustomer.FirstName;
            Lastname = _addedCustomer.NewCustomer.LastName;
            Birthday = (DateTime)_addedCustomer.NewCustomer.Birthday;
            Email = _addedCustomer.NewCustomer.Emailaddress;
            SelectedSubscription = (int)_addedCustomer.NewCustomer.Subscription - 1;

            City = _addedCustomer.NewCustomerAddress.City;
            Street = _addedCustomer.NewCustomerAddress.Streetname;
            Zipcode = _addedCustomer.NewCustomerAddress.Zipcode;
            Streetnumber = _addedCustomer.NewCustomerAddress.StreetNumber;
            //TrainingPlan.Refresh();
        }









        private List<string> _abo;

        public List<string> Abo
        {
            get { return _abo; }
            set { _abo = value; }
        }

        private int _seclectedSubscription = 0;

        public int SelectedSubscription
        {
            get { return _seclectedSubscription; }
            set
            {
                _seclectedSubscription = value;
                NotifyOfPropertyChange(() => _seclectedSubscription);
            }
        }

        public void GoBack()
        {
            _events.PublishOnUIThreadAsync(new BackEventModel());
        }

        public void AddPlan()
        {
            _addedCustomer.NewCustomer.FirstName = Firstname;
            _addedCustomer.NewCustomer.LastName = Lastname;
            _addedCustomer.NewCustomer.Createdate = DateTime.Now;
            _addedCustomer.NewCustomer.Birthday = Birthday;
            _addedCustomer.NewCustomer.Emailaddress = Email;
            _addedCustomer.NewCustomer.Subscription = SelectedSubscription + 1;

            _addedCustomer.NewCustomerAddress.City = City;
            _addedCustomer.NewCustomerAddress.Streetname = Street;
            _addedCustomer.NewCustomerAddress.Zipcode = Zipcode;
            _addedCustomer.NewCustomerAddress.StreetNumber = Streetnumber;

            _addedCustomer.NewCustomer = _addedCustomer.NewCustomer;
            _addedCustomer.NewCustomerAddress = _addedCustomer.NewCustomerAddress;

            _events.PublishOnUIThreadAsync(new AddPlanToNewCustomerEvent());
        }

        public void Add()
        {
            _addedCustomer.NewCustomer.FirstName = Firstname;
            _addedCustomer.NewCustomer.LastName = Lastname;
            _addedCustomer.NewCustomer.Createdate = DateTime.Now;
            _addedCustomer.NewCustomer.Birthday = Birthday;
            _addedCustomer.NewCustomer.Emailaddress = Email;
            _addedCustomer.NewCustomer.Subscription = SelectedSubscription + 1;

            _addedCustomer.NewCustomerAddress.City = City;
            _addedCustomer.NewCustomerAddress.Streetname = Street;
            _addedCustomer.NewCustomerAddress.Zipcode = Zipcode;
            _addedCustomer.NewCustomerAddress.StreetNumber = Streetnumber;



            int newAddressId;

            if (!ctx.Addresses.Any(x => x.Streetname.Equals(_addedCustomer.NewCustomerAddress.Streetname) && x.Zipcode.Equals(_addedCustomer.NewCustomerAddress.Zipcode) && x.StreetNumber.Equals(_addedCustomer.NewCustomerAddress.StreetNumber)))
            {
                ctx.Addresses.Add(_addedCustomer.NewCustomerAddress);
                ctx.SaveChanges();
            }

            newAddressId = ctx.Addresses.FirstOrDefault(x => x.Streetname.Equals(_addedCustomer.NewCustomerAddress.Streetname) && x.Zipcode.Equals(_addedCustomer.NewCustomerAddress.Zipcode) && x.StreetNumber.Equals(_addedCustomer.NewCustomerAddress.StreetNumber)).Id;

            ctx.Customers.Add(_addedCustomer.NewCustomer);
            ctx.SaveChanges();

            _addedCustomer.CustomerAddress.AddressId = newAddressId;
            _addedCustomer.CustomerAddress.CustomerId = ctx.Customers.FirstOrDefault(x => x.Equals(_addedCustomer.NewCustomer)).Id;

            ctx.CustomerAddresses.Add(_addedCustomer.CustomerAddress);
            ctx.SaveChanges();

            newAddressId = 0;

            MessageBox.Show($"Nutzer '{Firstname} {Lastname}' wurde erfolgreich angelegt!");

            _addedCustomer.NewCustomer = new();
            _addedCustomer.NewCustomerAddress = new();
            //Firstname = string.Empty;
            //Lastname = string.Empty;

            //Birthday = new(1999, 01, 01);
            //Email = string.Empty;

            //City = string.Empty;
            //Street = string.Empty;
            //Streetnumber = string.Empty;
            //Zipcode = null;

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

        private string _streetnumber;

        public string Streetnumber
        {
            get { return _streetnumber; }
            set
            {
                _streetnumber = value;
                NotifyOfPropertyChange(() => _streetnumber);
            }
        }


        private string _firstname;

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
                NotifyOfPropertyChange(() => _firstname);
            }
        }

        private string _lastname;

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
                NotifyOfPropertyChange(() => _lastname);
            }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyOfPropertyChange(() => _city);
            }
        }

        private string _street;

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                NotifyOfPropertyChange(() => _street);
            }
        }

        private int? _zipcode;

        public int? Zipcode
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;
                NotifyOfPropertyChange(() => _zipcode);
            }
        }

        private DateTime _birthday = new(1999, 01, 01);

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                NotifyOfPropertyChange(() => _birthday);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => _email);
            }
        }



    }
}
