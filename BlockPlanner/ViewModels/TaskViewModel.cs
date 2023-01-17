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

        public string Order => "10";

        public string TaskName
        {
            get => _task.TaskName;
            set => _task.TaskName = value;
        }

        public string WeekTime => string.Concat(_task.StartTime.ToString("d"), " - ", _task.EndTime.ToString("d"));

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

        public TaskViewModel(Task task)
        {
            _task = task;
        }
    }
}
