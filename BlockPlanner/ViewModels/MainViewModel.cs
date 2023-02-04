using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BlockPlanner.Models;
using BlockPlanner.Utilities;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        public MainViewModel(Scheduler scheduler)
        {
            SimulationInvoke(scheduler);
        }

        private void SimulationInvoke(Scheduler scheduler)
        {
            var simulationPlan = new Plan();
            simulationPlan.Name = "WeekPlan-01";
            simulationPlan.WeekStartTime = DateTimeUtilities.GetWeekStart(DateTime.Now);
            simulationPlan.WeekEndTime = DateTimeUtilities.GetWeekEnd(DateTime.Now);
            var testDayPlan = simulationPlan.ScheduledDays[WeekDay.Monday.GetId()];

            var today = DateTime.Now;
            var timeOffset = 10;
            today = today.AddHours(-today.Hour + timeOffset);

            var testTask = new Task(".NET", today, today.AddHours(1), (Color)ColorConverter.ConvertFromString("#86c676"), "AdditionalInfo");
            var testTask2 = new Task("OR", today.AddHours(2), today.AddHours(3), (Color)ColorConverter.ConvertFromString("#e5182b"), "AdditionalInfo");
            var testTask3 = new Task("SOC", today.AddHours(5), today.AddHours(6), (Color)ColorConverter.ConvertFromString("#8800e6"), "AdditionalInfo");
            testDayPlan.DayTasks.Add(testTask);
            testDayPlan.DayTasks.Add(testTask2);
            testDayPlan.DayTasks.Add(testTask3);
            CurrentViewModel = new PlanSettingsViewModel(simulationPlan);
        }
    }
}
