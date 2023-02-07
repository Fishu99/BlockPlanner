using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Models;

namespace BlockPlanner.Utilities
{
    public abstract class TaskUtilities
    {
        public static int CountTaskRow(DateTime taskStartTime)
        {
            var startDateTime = new DateTime(taskStartTime.Year, taskStartTime.Month, taskStartTime.Day, SchedulerSettings.StartTimeHour, 0, 0);

            return SchedulerSettings.GridRowOffset + CountTaskRowsBetween(startDateTime, taskStartTime);
        }

        public static int CountTaskRowSpan(DateTime taskStartTime, DateTime taskEndTime)
        {
            return CountTaskRowsBetween(taskStartTime, taskEndTime);
        }

        public static int CountTaskRowsBetween(DateTime taskStartTime, DateTime taskEndTime)
        {
            var startTimeStamp = (int)taskStartTime.TimeOfDay.TotalMinutes;
            var endTimeStamp= (int)taskEndTime.TimeOfDay.TotalMinutes;
            var elapsedTime = endTimeStamp - startTimeStamp;
            var elapsedTimeMinutes = elapsedTime;
            if (elapsedTimeMinutes % SchedulerSettings.TimeStampValue != 0)
            {
                Console.WriteLine("Tasks are incorrectly set.");
            }

            var rowsBetween = elapsedTimeMinutes/SchedulerSettings.TimeStampValue;
            return rowsBetween;
        }
    }
}
