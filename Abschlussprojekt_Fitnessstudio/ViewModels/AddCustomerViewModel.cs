using Abschlussprojekt_Fitnessstudio.DbModels;
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
        public Abschlussprojekt_FitnessstudioContext ctx = new();

        public AddCustomerViewModel(IEventAggregator events)
        {
            _events = events;
            Abo = new();
            ctx.Subscriptions.ToList().ForEach(x => Abo.Add(x.Name));
        }

        private Customer _newCustomer = new();


        private CustomerAddress _customerAddress = new();


        private Address _newCustomerAddress = new();

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


        public void Add()
        {
            _newCustomer.FirstName = Firstname;
            _newCustomer.LastName = Lastname;
            _newCustomer.Createdate = DateTime.Now;
            _newCustomer.Birthday = Birthday;
            _newCustomer.Emailaddress = Email;
            _newCustomer.Subscription = SelectedSubscription + 1;

            _newCustomerAddress.City = City;
            _newCustomerAddress.Streetname = Street;
            _newCustomerAddress.Zipcode = Zipcode;
            _newCustomerAddress.StreetNumber = Streetnumber;



            int ID;

            if (!ctx.Addresses.ToList().Exists(x => x.Streetname.Equals(_newCustomerAddress.Streetname) && x.Zipcode.Equals(_newCustomerAddress.Zipcode) && x.StreetNumber.Equals(_newCustomerAddress.StreetNumber)))
            {
                ctx.Addresses.Add(_newCustomerAddress);
                ctx.SaveChanges();
            }

            ID = ctx.Addresses.FirstOrDefault(x => x.Streetname.Equals(_newCustomerAddress.Streetname) && x.Zipcode.Equals(_newCustomerAddress.Zipcode) && x.StreetNumber.Equals(_newCustomerAddress.StreetNumber)).Id;

            ctx.Customers.Add(_newCustomer);
            ctx.SaveChanges();

            _customerAddress.AddressId = ID;
            _customerAddress.CustomerId = ctx.Customers.FirstOrDefault(x => x.Equals(_newCustomer)).Id;

            ctx.CustomerAddresses.Add(_customerAddress);
            ctx.SaveChanges();

            ID = 0;

            MessageBox.Show($"Nuter '{Firstname} {Lastname}' wurde erfolgreich angelegt!");


            //Firstname = string.Empty;
            //Lastname = string.Empty;

            //Birthday = new(1999, 01, 01);
            //Email = string.Empty;

            //City = string.Empty;
            //Street = string.Empty;
            //Streetnumber = string.Empty;
            //Zipcode = null;

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
