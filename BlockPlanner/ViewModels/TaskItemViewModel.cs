using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Models;
using BlockPlanner.Utilities;
using Task = BlockPlanner.Models.Task;

namespace BlockPlanner.ViewModels
{
    public class TaskItemViewModel
    {
        private Task _task;

        private string _name;
        private string _color;
        private int _row;
        private int _col;
        private int _rowSpan;

        public string Name => _name;
        public string Color => _color;
        public int Row => _row;
        public int Col => _col;
        public int RowSpan => _rowSpan;

        public TaskItemViewModel(Task task)
        {
            _task = task;
            _name = task.TaskName;
            _color = task.BlockColor.ToString();
            _row = TaskUtilities.CountTaskRow(task.StartTime);
            _col = WeekDayMethods.GetWeekDayIdFromDateTime(_task.StartTime);
            _rowSpan = TaskUtilities.CountTaskRowSpan(_task.StartTime, _task.EndTime);
        }
    }
}
