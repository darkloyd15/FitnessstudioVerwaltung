using Abschlussprojekt_Fitnessstudio.DbModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abschlussprojekt_Fitnessstudio.ViewModels
{
    class ScheduleViewModel : Screen
    {
        Abschlussprojekt_FitnessstudioContext _ctx = new();

        public ScheduleViewModel()
        {
            Schedules = new(_ctx.Schedules);
        }

        private BindableCollection<Schedule> _schedules;

        public BindableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                NotifyOfPropertyChange(() => _schedules);
            }
        }

    }
}
