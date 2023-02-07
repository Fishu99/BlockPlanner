using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public TaskDetailsViewModel(TaskDetailsViewModel taskDetailsViewModel)
        {
            _task = new Task(taskDetailsViewModel.TaskName, taskDetailsViewModel.StartTime,
                taskDetailsViewModel.EndTime, taskDetailsViewModel.BlockColor, taskDetailsViewModel.AdditionalInfo);

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

        public static TaskDetailsViewModel GetDefaultTask(DateTime selectedDay)
        {
            const int timeHourOffset = 12;
            var taskTime = new DateTime(selectedDay.Year, selectedDay.Month, selectedDay.Day, timeHourOffset, selectedDay.Minute, 0);
            taskTime = DateTimeUtilities.ValidateTaskTimeStamp(taskTime);

            var task = new Task("NewTask", taskTime, taskTime.AddMinutes(45), ColorUtilities.GetRandomColor(), "NewTask additional informations");
            var taskDetailsViewModel = new TaskDetailsViewModel(task);
            return taskDetailsViewModel;
        }
    }
}
