using BlockPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using BlockPlanner.Exceptions;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        protected Task _task;
        private string _taskOrder = "1";
        private bool _isSelected;

        public string Order
        {
            get => _taskOrder;
            set
            {
                _taskOrder = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public string TaskName
        {
            get => _task.TaskName;
            set
            {
                _task.TaskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        public string WeekTime => string.Concat(_task.StartTime.ToString("d"), " - ", _task.EndTime.ToString("d"));
        public string TimeSchedule => string.Concat(_task.StartTime.ToString("t"), " - ", _task.EndTime.ToString("t"));

        public DateTime StartTime
        {
            get => _task.StartTime;
            set
            {
                _task.StartTime = value;
                OnPropertyChanged(nameof(StartTime));
                OnPropertyChanged(nameof(WeekTime));
                OnPropertyChanged(nameof(TimeSchedule));
            }
        }
        public DateTime EndTime
        {
            get => _task.EndTime;
            set
            {
                _task.EndTime = value;
                OnPropertyChanged(nameof(EndTime));
                OnPropertyChanged(nameof(WeekTime));
                OnPropertyChanged(nameof(TimeSchedule));
            }
        }

        public string Color
        {
            get => _task.BlockColor.ToString();
            set
            {
                try
                {
                    var colorFromValue = (Color)ColorConverter.ConvertFromString(value);
                    _task.BlockColor = colorFromValue;
                    OnPropertyChanged(nameof(BlockColor));
                }
                catch (NullReferenceException)
                {
                    throw new ColorConversionFailedException();
                }
            }
        }

        public Color BlockColor
        {
            get => _task.BlockColor;
            set
            {
                _task.BlockColor = value;
                Color = value.ToString();
                OnPropertyChanged(nameof(BlockColor));
                OnPropertyChanged(nameof(Color));
            }
        }

        public bool IsGroovy
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsGroovy));
            }
        }

        public TaskViewModel()
        {
            _task = new Task();
        }

        public TaskViewModel(Task task)
        {
            _task = new Task(
                task.TaskName,
                task.StartTime,
                task.EndTime,
                task.BlockColor,
                task.AdditionalInfo);
        }

        public void UpdateDataFromTask(Task newTaskData)
        {
            if (newTaskData != null)
            {
                TaskName = newTaskData.TaskName;
                StartTime = newTaskData.StartTime;
                EndTime = newTaskData.EndTime;
                BlockColor = newTaskData.BlockColor;
            }
        }

        public void UpdateDataFromTask(Task newTaskData, int newOrderId)
        {
            if (newTaskData != null)
            {
                UpdateDataFromTask(newTaskData);
                Order = newOrderId.ToString();
            }
        }
        public static void ValidateStartAndEndTime(TaskDetailsViewModel taskDetails)
        {
            if (taskDetails == null)
            {
                return;
            }

            if (taskDetails.StartTime.TimeOfDay > taskDetails.EndTime.TimeOfDay)
            {
                (taskDetails.StartTime, taskDetails.EndTime) = (taskDetails.EndTime, taskDetails.StartTime);
            }
        }
    }
}
