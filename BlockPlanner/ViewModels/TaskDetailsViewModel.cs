using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BlockPlanner.Utilities;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class TaskDetailsViewModel : TaskViewModel
    {
        public string AdditionalInfo
        {
            get => _task.AdditionalInfo;
            set
            {
                _task.AdditionalInfo = value;
                OnPropertyChanged(nameof(AdditionalInfo));
            }
        }

        public TaskDetailsViewModel()
        {
            
        }

        public TaskDetailsViewModel(Task task) : base(task)
        {
            AdditionalInfo = task.AdditionalInfo;
        }

        public TaskDetailsViewModel(TaskDetailsViewModel taskDetailsViewModel) : base()
        {
            TaskName = taskDetailsViewModel.TaskName;
            StartTime = taskDetailsViewModel.StartTime;
            EndTime = taskDetailsViewModel.EndTime;
            Color = taskDetailsViewModel.Color;
            BlockColor = taskDetailsViewModel.BlockColor;
            AdditionalInfo = taskDetailsViewModel.AdditionalInfo;
            Order = taskDetailsViewModel.Order;
        }

        public TaskDetailsViewModel(Task task, int order) : base(task)
        {
            this.Order = order.ToString();
        }

        public void UpdateTaskChanges(Task taskToModify)
        {
            TaskName = taskToModify.TaskName;
            StartTime = taskToModify.StartTime;
            EndTime = taskToModify.EndTime;
            Color = taskToModify.BlockColor.ToString();
            BlockColor = taskToModify.BlockColor;
            AdditionalInfo = taskToModify.AdditionalInfo;
        }

        public void AllPropertyChangedEventInvoke()
        {
            OnPropertyChanged(nameof(TaskName));
            OnPropertyChanged(nameof(StartTime));
            OnPropertyChanged(nameof(EndTime));
            OnPropertyChanged(nameof(BlockColor));
            OnPropertyChanged(nameof(AdditionalInfo));
            OnPropertyChanged(nameof(TaskName));
        }

        public static TaskDetailsViewModel GetDefaultTask()
        {
            const int timeOffset = 12;
            var today = DateTime.Now;
            today = today.AddHours(-today.Hour + timeOffset);

            var task = new Task("NewTask", today, today, ColorUtilities.GetRandomColor(), "NewTask additional informations");
            var taskDetailsViewModel = new TaskDetailsViewModel(task);
            return taskDetailsViewModel;
        }
    }
}
