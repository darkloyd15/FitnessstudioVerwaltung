using Caliburn.Micro;
using Abschlussprojekt_Fitnessstudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Abschlussprojekt_Fitnessstudio.Models;

namespace Abschlussprojekt_Fitnessstudio
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<ISelectedCustomer,SelectedCustomer>()
                //.Singleton<IAddedTrainingPlanModel,AddedTrainingPlanModel>()
                .Singleton<IAddedCustomerModel,AddedCustomerModel>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType));

        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
