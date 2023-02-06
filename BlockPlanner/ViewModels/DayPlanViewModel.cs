using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Models;

namespace BlockPlanner.ViewModels
{
    public class DayPlanViewModel : ViewModelBase
    {
        private WeekDay _day;
        private ObservableCollection<TaskItemViewModel> _tasks;

        public WeekDay Day => _day;

        public ObservableCollection<TaskItemViewModel> Tasks => _tasks;


        public DayPlanViewModel(WeekDay weekDay)
        {
            _day = weekDay;
            _tasks = new ObservableCollection<TaskItemViewModel>();
        }
    }
}
