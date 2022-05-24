using Abschlussprojekt_Fitnessstudio.DbModels;
using Abschlussprojekt_Fitnessstudio.Models;
using Caliburn.Micro;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    class ChangeTrainigPlanViewModel : Screen
    {
        private readonly ISelectedCustomer _customer;
        public Abschlussprojekt_FitnessstudioContext ctx = new();

        public ChangeTrainigPlanViewModel(ISelectedCustomer customer)
        {
            _customer = customer;
            TrainingPlan = new BindableCollection<TrainingMachinePlan>(ctx.TrainingMachinePlans.Where(x => x.TrainingPlanId.Equals(customer.CurrentCustomer.TrainingPlanId)).Include(c => c.TraininMachine));
        }



        public void Remove(TrainingMachinePlan child)
        {
            TrainingPlan.Remove(child);
            TrainingPlan.Refresh();
        }

        public void Add()
        {
            if (TrainingPlan.Any(x => x.TraininMachine.Name == MachineName))
            {
                if (TrainingPlan.First(x => x.TraininMachine.Name == MachineName).Iteration + Iterations < 0)
                {
                    MessageBox.Show("Wiederholungen Können nicht kleiner als 0 sein!");
                    return;
                }
                TrainingPlan.First(x => x.TraininMachine.Name == MachineName).Iteration += Iterations;
                TrainingPlan.Refresh();
            }
            else
            {
                TrainingMachine newMachine = new();
                TrainingMachinePlan newPlan = new();

                if (!ctx.TrainingMachines.Any(x => x.Name == MachineName))
                {
                    newMachine.Name = MachineName;
                    ctx.TrainingMachines.Add(newMachine);
                    ctx.SaveChanges();
                    newMachine.Id = ctx.TrainingMachines.First(x => x.Name.Equals(newMachine.Name)).Id;
                }
                else
                {
                    newMachine = ctx.TrainingMachines.First(x => x.Name.Equals(MachineName));
                }

                newPlan.TraininMachine = newMachine;
                newPlan.TraininMachineId = newMachine.Id;
                newPlan.TrainingPlan = TrainingPlan.First().TrainingPlan;
                newPlan.TrainingPlanId = TrainingPlan.First().TrainingPlanId;
                newPlan.Iteration = Iterations;

                TrainingPlan.Add(newPlan);
                TrainingPlan.Refresh();
            }
        }

        public void ChangePlan()
        {

        }

        private string _machineName;

        public string MachineName
        {
            get { return _machineName; }
            set
            {
                _machineName = value;
                NotifyOfPropertyChange(() => _machineName);
            }
        }

        private int _iterations;

        public int Iterations
        {
            get { return _iterations; }
            set
            {
                _iterations = value;
                NotifyOfPropertyChange(() => _iterations);
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
