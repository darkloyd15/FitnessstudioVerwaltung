using Abschlussprojekt_Fitnessstudio.DbModels;
using Abschlussprojekt_Fitnessstudio.EventModels;
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

        public ScheduleViewModel(IEventAggregator events)
        {
            Schedules = new(_ctx.Schedules);
            _events = events;
        }

        public void GoBack()
        {
            _events.PublishOnUIThreadAsync(new BackEventModel());
        }

        private BindableCollection<Schedule> _schedules;
        private readonly IEventAggregator _events;

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
