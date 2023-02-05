using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlockPlanner.Models;

namespace BlockPlanner.Utilities
{
    public abstract class DateTimeUtilities
    {
        private const int WeekLength = 7;
        public static string GetWeekRange(DateTime weekDayDate)
        {
            var weekDateTime = GetWeekStart(weekDayDate);

            var weekStartDate = weekDateTime.ToString("dd/MM/yyyy");
            weekDateTime = weekDateTime.AddDays(WeekLength-1);
            var weekEndDate = weekDateTime.ToString("dd/MM/yyyy");

            return string.Concat(weekStartDate, " - ", weekEndDate);
        }

        public static DateTime GetWeekStart(DateTime weekDayDate)
        {
            var dayOfTheWeek = weekDayDate.DayOfWeek == DayOfWeek.Sunday ? WeekLength : (int)weekDayDate.DayOfWeek;
            var subtractToWeekStart = dayOfTheWeek - 1;
            return weekDayDate.AddDays(-subtractToWeekStart);
        }

        public static DateTime GetWeekEnd(DateTime weekDayDate)
        {
            var weekStart = GetWeekStart(weekDayDate);
            return weekStart.AddDays(WeekLength-1);
        }

        public static DateTime ExtractWeekStartFromString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return DateTime.Now;
            }
            var regex = new Regex(@"[\d]+");
            var matches = regex.Matches(dateString);
            if (matches.Count != 6) return DateTime.Now;
            var day = int.Parse(matches[0].Value);
            var month = int.Parse(matches[1].Value);
            var year = int.Parse(matches[2].Value);
            var dateTime = new DateTime(year, month, day);

            return dateTime;
        }

        public static int GetWeekDayId(DateTime date)
        {
            var rawId = date.DayOfWeek == DayOfWeek.Sunday ? WeekLength : (int)date.DayOfWeek;
            return rawId - 1;
        }

        public static WeekDay GetWeekDay(DateTime date)
        {
            return (WeekDay)GetWeekDayId(date);
        }

        public static DateTime ValidateTaskTimeStamp(string taskTime)
        {
            var timeValue = DateTime.Parse(taskTime);
            return ValidateTaskTimeStamp(timeValue);
        }

        public static DateTime ValidateTaskTimeStamp(DateTime taskTime)
        {
            var minMod15 = taskTime.Minute % 15;
            if (minMod15 != 0)
            {
                taskTime = taskTime.AddMinutes(minMod15 < 8 ? -minMod15 : 15 - minMod15);
            }

            return taskTime;
        }
    }
}
