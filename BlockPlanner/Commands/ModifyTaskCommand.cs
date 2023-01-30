using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.Exceptions;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.Commands
{
    public class ModifyTaskCommand : CommandBase
    {
        private readonly PlanSettingsViewModel _planSettingsViewModel;
        private TaskDetailsViewModel _selectedTask;

        public ModifyTaskCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            _planSettingsViewModel = planSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            _selectedTask = _planSettingsViewModel.SelectedTask;
            if (_selectedTask == null || _planSettingsViewModel.CurrentTasks.Count == 0)
            {
                MessageBox.Show("The task could not be modified because none was selected (or not exists)", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var task = new Task(_selectedTask.TaskName,
                _selectedTask.StartTime,
                _selectedTask.EndTime,
                _selectedTask.BlockColor,
                _selectedTask.AdditionalInfo);
            var taskToModifyId = int.Parse(_selectedTask.Order) - 1;

            List<Task> dayTasks = _planSettingsViewModel.CurrentDayPlan.DayTasks;
            Task taskToModify = dayTasks[taskToModifyId];
            TaskDetailsViewModel taskToModifyFromView = (TaskDetailsViewModel)_planSettingsViewModel.CurrentTasks[taskToModifyId];
            try
            {
                bool isChanged = taskToModify.UpdateTaskInformation(task, dayTasks, out int placementId);
                if (isChanged)
                {
                    taskToModifyFromView.UpdateTaskChanges(taskToModify);

                    //Update task in list placements
                    _planSettingsViewModel.CurrentTasks.Move(taskToModifyId, placementId);
                    _planSettingsViewModel.UpdateOrderId();
                    _planSettingsViewModel.CurrentDayPlan.DayTasks.RemoveAt(taskToModifyId);
                    _planSettingsViewModel.CurrentDayPlan.DayTasks.Insert(int.Parse(taskToModifyFromView.Order)-1, taskToModify);
                    _planSettingsViewModel.SelectedTask = new TaskDetailsViewModel(taskToModifyFromView);
                    MessageBox.Show("Successfully modified task.", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("New data is the same - nothing to modify.", "Success", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (TaskCollisionException)
            {
                MessageBox.Show("Modified task time interval conflicts with one of the existing tasks", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCurrentTaskEquivalent(int taskId, int newTaskId, Task newData)
        {
            ObservableCollection<TaskViewModel> currentTasks = _planSettingsViewModel.CurrentTasks;
            if (currentTasks == null)
            {
                throw new ArgumentNullException(nameof(currentTasks));
            }

            if (taskId >= currentTasks.Count)
            {
                throw new ArgumentNullException(nameof(taskId));
            }

            TaskViewModel currentTaskViewModelInList = currentTasks[taskId];
            currentTaskViewModelInList.UpdateDataFromTask(newData, newTaskId);
            currentTasks.Move(taskId, newTaskId);
        }
    }
}
