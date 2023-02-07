using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Models
{
    public abstract class SchedulerSettings
    {
        public const int StartTimeHour = 6;
        public const int EndTimeHour = 21;
        public const int TimeStampValue = 15;
        public const int GridRowOffset = 1;
    }
}
