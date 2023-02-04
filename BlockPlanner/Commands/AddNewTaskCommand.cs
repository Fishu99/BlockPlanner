using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlockPlanner.Exceptions;
using BlockPlanner.Models;
using BlockPlanner.ViewModels;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.Commands
{
    public class AddNewTaskCommand: CommandBase
    {
        private PlanSettingsViewModel _planSettingsViewModel;
        private TaskDetailsViewModel _newTaskDetails;

        public AddNewTaskCommand(PlanSettingsViewModel planSettingsViewModel)
        {
            _planSettingsViewModel = planSettingsViewModel;

            planSettingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool canExecute = true;

            if (//string.IsNullOrEmpty(_taskDetailsViewModel.TaskName) && 
                !base.CanExecute(parameter))
            {
                canExecute = false;
            }

            return canExecute;
        }

        public override void Execute(object parameter)
        {
            _newTaskDetails = _planSettingsViewModel.SelectedTask;
            TaskViewModel.ValidateStartAndEndTime(_newTaskDetails);
            var task = new Task(_newTaskDetails.TaskName,
                _newTaskDetails.StartTime,
                _newTaskDetails.EndTime,
                _newTaskDetails.BlockColor,
                _newTaskDetails.AdditionalInfo);

            try
            {
                DayPlan curPlan = _planSettingsViewModel.CurrentDayPlan;
                curPlan.AddNewTask(task, out int placementId);
                UpdateCurrentTasks(task, placementId);

                MessageBox.Show("Successfully added new task.", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (TaskCollisionException)
            {
                MessageBox.Show("New task time interval conflicts with one of the existing tasks", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        

        private void UpdateCurrentTasks(Task task, int placementId)
        {
            ObservableCollection<TaskViewModel> currentTasks = _planSettingsViewModel.CurrentTasks;
            if (currentTasks == null)
            {
                throw new NullReferenceException("CurrentTasks were null");
            }
            TaskDetailsViewModel newTask = new TaskDetailsViewModel(task);
            newTask.PropertyChanged += AddTaskSelectionSubscription;
            currentTasks.Insert(placementId, newTask);
            _planSettingsViewModel.UpdateOrderId();
            _planSettingsViewModel.SelectedTask = new TaskDetailsViewModel(newTask);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (
                e.PropertyName == nameof(TaskDetailsViewModel.TaskName) ||
                e.PropertyName == nameof(TaskDetailsViewModel.StartTime) ||
                e.PropertyName == nameof(TaskDetailsViewModel.EndTime) ||
                e.PropertyName == nameof(TaskDetailsViewModel.Color) ||
                e.PropertyName == nameof(TaskDetailsViewModel.AdditionalInfo)
            )
            {
                OnCanExecutedChanged();
            }
        }

        private void AddTaskSelectionSubscription(object sender, PropertyChangedEventArgs args)
        {
            var senderObj = (TaskDetailsViewModel)sender;
            if (args.PropertyName != "IsGroovy" || !senderObj.IsGroovy) return;

            Console.WriteLine("New task selected (" + _newTaskDetails.StartTime + ")");

            TaskDetailsViewModel taskDetails = new TaskDetailsViewModel(senderObj);

            _planSettingsViewModel.SelectedTask = taskDetails;
            _planSettingsViewModel.TaskName = taskDetails.TaskName;
            _planSettingsViewModel.StartTime = taskDetails.StartTime.ToString("t");
            _planSettingsViewModel.EndTime = taskDetails.EndTime.ToString("t");
            _planSettingsViewModel.Color = taskDetails.Color;
            _planSettingsViewModel.AdditionalInfo = taskDetails.AdditionalInfo;

            Console.WriteLine("Now is (" + _newTaskDetails.StartTime + ")");
        }
    }
}
