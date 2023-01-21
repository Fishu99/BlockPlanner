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
            set => _task.TaskName = value;
        }

        public string WeekTime => string.Concat(_task.StartTime.ToString("d"), " - ", _task.EndTime.ToString("d"));
        public string TimeSchedule => string.Concat(_task.StartTime.ToString("t"), " - ", _task.EndTime.ToString("t"));

        public DateTime StartTime
        {
            get => _task.StartTime;
            set => _task.StartTime = value;
        }
        public DateTime EndTime
        {
            get => _task.EndTime;
            set => _task.EndTime = value;
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
                OnPropertyChanged(nameof(BlockColor));
            }
        }

        public TaskViewModel(Task task)
        {
            //_task = task;
            _task = new Task(
                task.TaskName,
                task.StartTime,
                task.EndTime,
                task.BlockColor,
                task.AdditionalInfo);
        }
    }
}
