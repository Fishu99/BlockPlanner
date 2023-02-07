using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BlockPlanner.Models;
using BlockPlanner.Stores;
using BlockPlanner.Utilities;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
        
            set
            {
                _navigationStore.CurrentViewModel = value;
            }
        }

        public MainViewModel(Scheduler scheduler, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public static Plan SimulationInvoke()
        {
            var simulationPlan = new Plan
            {
                Name = "OS - summer semester",
                WeekStartTime = DateTimeUtilities.GetWeekStart(DateTime.Now),
                WeekEndTime = DateTimeUtilities.GetWeekEnd(DateTime.Now)
            };
            
            /*
             TaskColors:
            #0A9BFA - Blue -> labs
            #4BF00A - Green -> lectures
            #FA8C0A - Orange -> taskClasses
            #DB02E3 - Purple -> projectClasses             
             */

            var baseTime = simulationPlan.WeekStartTime;

            var mondayPlan = simulationPlan.ScheduledDays[WeekDay.Monday.GetId()];
            baseTime = baseTime.AddHours(-baseTime.Hour + SchedulerSettings.StartTimeHour);  // 6:00
            var mondayTask = new Task("IK", DateTimeUtilities.GetTaskDateTime(baseTime,8,0), DateTimeUtilities.GetTaskDateTime(baseTime, 11, 45), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            var mondayTask2 = new Task("JO", DateTimeUtilities.GetTaskDateTime(baseTime, 12, 0), DateTimeUtilities.GetTaskDateTime(baseTime, 13, 30), (Color)ColorConverter.ConvertFromString("#FA8C0A"), "AdditionalInfo");
            var mondayTask3 = new Task("PPP", DateTimeUtilities.GetTaskDateTime(baseTime, 14, 0), DateTimeUtilities.GetTaskDateTime(baseTime, 17,0), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            mondayPlan.DayTasks.Add(mondayTask);
            mondayPlan.DayTasks.Add(mondayTask2);
            mondayPlan.DayTasks.Add(mondayTask3);

            var tuesdayPlan = simulationPlan.ScheduledDays[WeekDay.Tuesday.GetId()];
            baseTime = baseTime.AddDays(1);
            var tuesdayTask = new Task("PAI", DateTimeUtilities.GetTaskDateTime(baseTime, 8, 0), DateTimeUtilities.GetTaskDateTime(baseTime, 11,00), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            var tuesdayTask2 = new Task("PPP", DateTimeUtilities.GetTaskDateTime(baseTime, 11, 15), DateTimeUtilities.GetTaskDateTime(baseTime, 12, 30), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            var tuesdayTask3 = new Task("PAI", DateTimeUtilities.GetTaskDateTime(baseTime, 12, 45), DateTimeUtilities.GetTaskDateTime(baseTime, 14, 15), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            var tuesdayTask4 = new Task(".NET", DateTimeUtilities.GetTaskDateTime(baseTime, 14, 30), DateTimeUtilities.GetTaskDateTime(baseTime, 20, 30), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            tuesdayPlan.DayTasks.Add(tuesdayTask);
            tuesdayPlan.DayTasks.Add(tuesdayTask2);
            tuesdayPlan.DayTasks.Add(tuesdayTask3);
            tuesdayPlan.DayTasks.Add(tuesdayTask4);

            var wednesdayPlan = simulationPlan.ScheduledDays[WeekDay.Wednesday.GetId()];
            baseTime = baseTime.AddDays(1);
            var wednesdayTask = new Task("MC", DateTimeUtilities.GetTaskDateTime(baseTime, 8, 30), DateTimeUtilities.GetTaskDateTime(baseTime, 10, 00), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            wednesdayPlan.DayTasks.Add(wednesdayTask);

            var thursdayPlan = simulationPlan.ScheduledDays[WeekDay.Thursday.GetId()];
            baseTime = baseTime.AddDays(1);
            var thursdayTask = new Task("ORII", DateTimeUtilities.GetTaskDateTime(baseTime, 8, 0), DateTimeUtilities.GetTaskDateTime(baseTime, 11,00), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            var thursdayTask2 = new Task("ORII", DateTimeUtilities.GetTaskDateTime(baseTime, 11, 15), DateTimeUtilities.GetTaskDateTime(baseTime, 12, 30), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            var thursdayTask3 = new Task("PAUM", DateTimeUtilities.GetTaskDateTime(baseTime, 13, 15), DateTimeUtilities.GetTaskDateTime(baseTime, 14, 45), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            var thursdayTask4 = new Task("PAUM", DateTimeUtilities.GetTaskDateTime(baseTime, 15, 00), DateTimeUtilities.GetTaskDateTime(baseTime, 16, 15), (Color)ColorConverter.ConvertFromString("#DB02E3"), "AdditionalInfo");
            var thursdayTask5 = new Task("OiRPOS", DateTimeUtilities.GetTaskDateTime(baseTime, 17, 00), DateTimeUtilities.GetTaskDateTime(baseTime, 18, 15), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            var thursdayTask6 = new Task(".NET", DateTimeUtilities.GetTaskDateTime(baseTime, 18, 30), DateTimeUtilities.GetTaskDateTime(baseTime, 20,0), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            thursdayPlan.DayTasks.Add(thursdayTask);
            thursdayPlan.DayTasks.Add(thursdayTask2);
            thursdayPlan.DayTasks.Add(thursdayTask3);
            thursdayPlan.DayTasks.Add(thursdayTask4);
            thursdayPlan.DayTasks.Add(thursdayTask5);
            thursdayPlan.DayTasks.Add(thursdayTask6);

            var fridayPlan = simulationPlan.ScheduledDays[WeekDay.Friday.GetId()];
            baseTime = baseTime.AddDays(1);
            var fridayTask = new Task("MC", DateTimeUtilities.GetTaskDateTime(baseTime, 8, 0), DateTimeUtilities.GetTaskDateTime(baseTime, 10, 45), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            var fridayTask2 = new Task("ORII", DateTimeUtilities.GetTaskDateTime(baseTime, 12, 00), DateTimeUtilities.GetTaskDateTime(baseTime, 15, 0), (Color)ColorConverter.ConvertFromString("#0A9BFA"), "AdditionalInfo");
            var fridayTask3 = new Task("SOC", DateTimeUtilities.GetTaskDateTime(baseTime, 15, 30), DateTimeUtilities.GetTaskDateTime(baseTime, 18, 30), (Color)ColorConverter.ConvertFromString("#4BF00A"), "AdditionalInfo");
            fridayPlan.DayTasks.Add(fridayTask);
            fridayPlan.DayTasks.Add(fridayTask2);
            fridayPlan.DayTasks.Add(fridayTask3);


            return simulationPlan;
        }
    }
}
