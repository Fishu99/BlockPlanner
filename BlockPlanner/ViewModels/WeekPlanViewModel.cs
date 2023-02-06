using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;

namespace BlockPlanner.ViewModels
{
    public class WeekPlanViewModel
    {
        private readonly Plan _plan;
        private DayPlanViewModel _mondayDayPlan;
        private DayPlanViewModel _tuesdayDayPlan;
        private DayPlanViewModel _wednesdayPlan;
        private DayPlanViewModel _thursdayDayPlan;
        private DayPlanViewModel _fridayDayPlan;
        private DayPlanViewModel _saturdayPlan;
        private DayPlanViewModel _sundayDayPlan;

        public ObservableCollection<TaskItemViewModel> MondayDayPlan => _mondayDayPlan.Tasks;
        public ObservableCollection<TaskItemViewModel> TuesdayDayPlan => _tuesdayDayPlan.Tasks;
        public ObservableCollection<TaskItemViewModel> WednesdayPlan => _wednesdayPlan.Tasks;
        public ObservableCollection<TaskItemViewModel> ThursdayDayPlan => _thursdayDayPlan.Tasks;
        public ObservableCollection<TaskItemViewModel> FridayDayPlan => _fridayDayPlan.Tasks;
        public ObservableCollection<TaskItemViewModel> SaturdayPlan => _saturdayPlan.Tasks;
        public ObservableCollection<TaskItemViewModel> SundayDayPlan => _sundayDayPlan.Tasks;

        public WeekPlanViewModel(Plan plan)
        {
            _plan = plan;
            InitializeDayPlans();
            ReadTaskData();
        }

        private void ReadTaskData()
        {
            if (_plan == null)
            {
                Console.WriteLine("There is no data to read.");
                return;
            }

            for (var i = WeekDay.Monday.GetId(); i < WeekDay.Sunday.GetId() + 1; i++)
            {
                var daySchedule = _plan.ScheduledDays[i];
                if (daySchedule == null)
                {
                    continue;
                }

                var dayTasks = daySchedule.DayTasks;
                if (dayTasks == null)
                {
                    continue;
                }

                for (var taskId = 0; taskId < dayTasks.Count; taskId++)
                {
                    var task = dayTasks[taskId];
                    var taskItemViewModel = new TaskItemViewModel(task);
                    AddTaskItemToList(taskItemViewModel, i);
                }
            }

        }

        private void AddTaskItemToList(TaskItemViewModel newTask, int dayId)
        {
            var weekDay = (WeekDay)dayId;
            switch (weekDay)
            {
                case WeekDay.Monday:
                    MondayDayPlan.Add(newTask);
                    break;
                case WeekDay.Tuesday:
                    TuesdayDayPlan.Add(newTask);
                    break;
                case WeekDay.Wednesday:
                    WednesdayPlan.Add(newTask);
                    break;
                case WeekDay.Thursday:
                    ThursdayDayPlan.Add(newTask);
                    break;
                case WeekDay.Friday:
                    FridayDayPlan.Add(newTask);
                    break;
                case WeekDay.Saturday:
                    SaturdayPlan.Add(newTask);
                    break;
                case WeekDay.Sunday:
                    SundayDayPlan.Add(newTask);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitializeDayPlans()
        {
            _mondayDayPlan = new DayPlanViewModel(WeekDay.Monday);
            _tuesdayDayPlan = new DayPlanViewModel(WeekDay.Tuesday);
            _wednesdayPlan = new DayPlanViewModel(WeekDay.Wednesday);
            _thursdayDayPlan = new DayPlanViewModel(WeekDay.Thursday);
            _fridayDayPlan = new DayPlanViewModel(WeekDay.Friday);
            _saturdayPlan = new DayPlanViewModel(WeekDay.Saturday);
            _sundayDayPlan = new DayPlanViewModel(WeekDay.Sunday);
        }
    }
}
