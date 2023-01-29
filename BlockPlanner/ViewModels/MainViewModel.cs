using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BlockPlanner.Models;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel(Scheduler scheduler)
        {
            //CurrentViewModel = new MainMenuViewModel();
            List<DayPlan> scheduledDayPlans = new List<DayPlan>();
            DayPlan testDayPlan = new DayPlan(WeekDay.Monday);
            scheduledDayPlans.Add(testDayPlan);

            DateTime today = DateTime.Now;
            int timeOffset = 10;
            today = today.AddHours(-today.Hour + timeOffset);


            Task testTask = new Task(".NET", today, today.AddHours(1), (Color)ColorConverter.ConvertFromString("#86c676"), "AdditionalInfo");
            Task testTask2 = new Task("OR", today.AddHours(2), today.AddHours(3), (Color)ColorConverter.ConvertFromString("#e5182b"), "AdditionalInfo");
            Task testTask3 = new Task("SOC", today.AddHours(5), today.AddHours(6), (Color)ColorConverter.ConvertFromString("#8800e6"), "AdditionalInfo");
            testDayPlan.DayTasks.Add(testTask);
            testDayPlan.DayTasks.Add(testTask2);
            testDayPlan.DayTasks.Add(testTask3);
            CurrentViewModel = new PlanSettingsViewModel(new Plan("WeekPlan-01", DateTime.Now, DateTime.Now, scheduledDayPlans));
        }
    }
}
