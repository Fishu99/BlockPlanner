using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.ViewModels;

namespace BlockPlanner.Commands
{
    public class DeleteTaskCommand : CommandBase
    {
        private readonly PlanSettingsViewModel _planSettingsViewModel;

        public DeleteTaskCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            _planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            var selectedTaskDetails = _planSettingsViewModel.SelectedTask;
            if (selectedTaskDetails == null || _planSettingsViewModel.CurrentTasks.Count == 0)
            {
                MessageBox.Show("The task could not be deleted because none was selected (or not exists)", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var taskName = selectedTaskDetails.TaskName;
            var orderId = int.Parse(selectedTaskDetails.Order);
            var taskId = orderId - 1;
            var tasksList = _planSettingsViewModel.CurrentDayPlan.DayTasks;
            var tasksViewList = _planSettingsViewModel.CurrentTasks;

            tasksList.RemoveAt(taskId);
            tasksViewList.RemoveAt(taskId);

            _planSettingsViewModel.UpdateOrderId();
            if (tasksList.Count > 0)
            {
                _planSettingsViewModel.SelectedTask = (TaskDetailsViewModel)tasksViewList.Last();
            }
            else
            {
                _planSettingsViewModel.SelectedTask = TaskDetailsViewModel.GetDefaultTask();
            }

            MessageBox.Show($"Task {taskName} successfully deleted", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
