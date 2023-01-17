using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BlockPlanner.Models
{
    public class Task
    {
        public string TaskName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Color BlockColor { get; set; }

        public string AdditionalInfo { get; set; }

        public Task()
        {
        }

        public Task(string taskName, DateTime startTime, DateTime endTime, Color blockColor, string additionalInfo)
        {
            TaskName = taskName;
            StartTime = startTime;
            EndTime = endTime;
            BlockColor = blockColor;
            AdditionalInfo = additionalInfo;
        }
    }
}
