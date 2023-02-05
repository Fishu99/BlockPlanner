using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.Exceptions;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Commands
{
    public class SaveAndExitCommand : CommandBase
    {
        private readonly PlanSettingsViewModel _planSettingsViewModel;

        public SaveAndExitCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            this._planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            var currentPlan = _planSettingsViewModel.Plan;
            var scheduler = _planSettingsViewModel.Scheduler;
            try
            {
                scheduler.AddNewPlan(currentPlan);
                _planSettingsViewModel.BackToPreviousViewCommand.Execute(null);
            }
            catch (SchedulePlanAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            // try
            // {
            //     DayPlan curPlan = _planSettingsViewModel.CurrentDayPlan;
            //     curPlan.AddNewTask(task, out int placementId);
            //     UpdateCurrentTasks(task, placementId);
            //
            //     MessageBox.Show("Successfully added new task.", "Success", MessageBoxButton.OK,
            //         MessageBoxImage.Information);
            // }
            // catch (TaskCollisionException)
            // {
            //     MessageBox.Show("New task time interval conflicts with one of the existing tasks", "Error",
            //         MessageBoxButton.OK, MessageBoxImage.Error);
            // }


        }
    }
}
