using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private TaskDetailsViewModel _selectedTask;

        public AddNewTaskCommand(List<DayPlan> scheduledDays, PlanSettingsViewModel planSettingsViewModel)
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
            _selectedTask = _planSettingsViewModel.SelectedTask;
            var task = new Task(_selectedTask.TaskName,
                _selectedTask.StartTime,
                _selectedTask.EndTime,
                _selectedTask.BlockColor,
                _selectedTask.AdditionalInfo);

            try
            {
                DayPlan curPlan = _planSettingsViewModel.CurrentDayPlan; //_scheduledDays[(int)_currentWeekDay];

                curPlan.AddNewTask(task, out int placementId);
                UpdateCurrentTasks(_planSettingsViewModel.CurrentTasks, task, placementId);

                MessageBox.Show("Successfully added new task.", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (TaskCollisionException ex)
            {
                MessageBox.Show("New task time interval conflicts with one of the existing tasks", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void UpdateCurrentTasks(ObservableCollection<TaskViewModel> currentTasks, Task task, int placementId)
        {
            if (currentTasks == null)
            {
                throw new NullReferenceException("CurrentTasks were null");
            }
            currentTasks.Insert(placementId, new TaskViewModel(task));
            for (var elementId = 0; elementId < currentTasks.Count; elementId++)
            {
                currentTasks[elementId].Order = (elementId + 1).ToString();
            }
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
    }
}
