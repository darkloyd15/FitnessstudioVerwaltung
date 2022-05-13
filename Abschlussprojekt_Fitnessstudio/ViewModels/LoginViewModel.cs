using Abschlussprojekt_Fitnessstudio.EventModels;
using Abschlussprojekt_Fitnessstudio.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Abschlussprojekt_Fitnessstudio.DbModels;
using System.Windows;

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    public class LoginViewModel : Screen
    {
        public Abschlussprojekt_FitnessstudioContext ctx = new();

        private readonly IEventAggregator _events;
        private readonly ILoggedInUserModel _user;

        public LoginViewModel(IEventAggregator events, ILoggedInUserModel user)
        {
            _events = events;
            _user = user;
        }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;

                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;

                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin
        {
            get
            {
                return !String.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
            }
        }

        public async Task Login()
        {
            string Firstname = UserName.Split('.').First(), Lastname = UserName.Split('.').Last();
            Employee LogOnUser = ctx.Employees.FirstOrDefault(x => x.FirstName.Equals(Firstname) && x.LastName.Equals(Lastname));
            if (LogOnUser == null || LogOnUser.Password != Password)
            {
                MessageBox.Show("Ungülitger Nutzername oder Passwort");
            }
            else
            {
                _user.FirstName = Firstname;
                _user.LastName= Lastname;
                try
                {
                    await _events.PublishOnUIThreadAsync(new LogInEvent());
                }
                catch (Exception)
                {

                    throw;
                }
            }            
        }

        public void OnPasswordChanged(PasswordBox source)
        {
            using var sha256 = SHA256.Create();
            // Hash PasswortBox
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source.Password));
            // Get the hashed string.  
            Password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
