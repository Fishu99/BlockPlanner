using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Exceptions;
using BlockPlanner.Models;
using BlockPlanner.Utilities;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Commands
{
    public class DaySelectCommand : CommandBase
    {
        private PlanSettingsViewModel _planSettingsViewModel;

        public DaySelectCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            _planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            var parameterText = (string)parameter;
            var parameterWeekDay = WeekDayMethods.TryGetWeekDay(parameterText);
            var parameterWeekDayId = parameterWeekDay.GetId();
            var currentDayId = _planSettingsViewModel.CurrentSelectedDayId;

            if (parameterWeekDayId == currentDayId)
            {   //If current selected day is the same as the day from the button click
                Console.WriteLine("Selected day is current day.");
            }
            else
            {
                //If there is need to change to other day than the one selected
                var dayPlansList = _planSettingsViewModel.Plan.ScheduledDays;
                if (dayPlansList.Count != WeekDayMethods.GetElementsCount())
                {
                    throw new InvalidSizeException("Invalid size of dayPlansList");
                }
                var newDayPlan = dayPlansList[parameterWeekDayId];

                if (newDayPlan == null)
                {
                    dayPlansList[parameterWeekDayId] = new DayPlan(parameterWeekDay);
                }
                _planSettingsViewModel.CurrentTasks = new ObservableCollection<TaskViewModel>();

                foreach (var dayTask in newDayPlan.DayTasks)
                {
                    var taskDetailsViewModel = new TaskDetailsViewModel(dayTask);
                    taskDetailsViewModel.PropertyChanged += _planSettingsViewModel.AddTaskSelectionSubscription;
                    _planSettingsViewModel.CurrentTasks.Add(taskDetailsViewModel);
                }

                _planSettingsViewModel.CurrentSelectedDayId = parameterWeekDayId;
                _planSettingsViewModel.UpdateOrderId();

                var date = DateTimeUtilities.ExtractWeekStartFromString(_planSettingsViewModel.WeekDateRange);
                _planSettingsViewModel.UpdateSelectedDay(date.AddDays(parameterWeekDayId));
            }

            

        }
    }
}
