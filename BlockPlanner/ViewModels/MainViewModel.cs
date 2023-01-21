﻿using System;
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
            Task testTask = new Task("TestowyTask", DateTime.Now.AddHours(7), DateTime.Now.AddHours(8), (Color)ColorConverter.ConvertFromString("#86c676"), "AdditionalInfo");
            Task testTask2 = new Task("TestowyTask2", DateTime.Now.AddHours(2), DateTime.Now.AddHours(2), (Color)ColorConverter.ConvertFromString("#e5182b"), "AdditionalInfo");
            Task testTask3 = new Task("TestowyTask3", DateTime.Now.AddHours(4), DateTime.Now.AddHours(2), (Color)ColorConverter.ConvertFromString("#8800e6"), "AdditionalInfo");
            testDayPlan.DayTasks.Add(testTask);
            testDayPlan.DayTasks.Add(testTask2);
            testDayPlan.DayTasks.Add(testTask3);
            CurrentViewModel = new PlanSettingsViewModel(new Plan("Tudududu", DateTime.Now, DateTime.Now, scheduledDayPlans));
        }
    }
}