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
            var startDateTime = new DateTime(taskStartTime.Ticks);
            startDateTime = startDateTime.AddHours(-startDateTime.Hour + SchedulerSettings.StartTimeHour);

            return CountTaskRowsBetween(startDateTime, taskStartTime);
        }

        public static int CountTaskRowSpan(DateTime taskStartTime, DateTime taskEndTime)
        {
            return CountTaskRowsBetween(taskStartTime, taskEndTime);
        }

        public static int CountTaskRowsBetween(DateTime taskStartTime, DateTime taskEndTime)
        {
            var startTimeStamp = taskStartTime.TimeOfDay;
            var endTimeStamp= taskEndTime.TimeOfDay;
            var elapsedTime = endTimeStamp - startTimeStamp;
            var elapsedTimeMinutes = elapsedTime.TotalMinutes;
            if (elapsedTimeMinutes % SchedulerSettings.TimeStampValue != 0)
            {
                Console.WriteLine("Tasks are incorrectly set.");
            }

            var rowsBetween = (int)(elapsedTimeMinutes/SchedulerSettings.TimeStampValue);
            return rowsBetween;
        }
    }
}
