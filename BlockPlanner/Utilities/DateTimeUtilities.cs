using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public static DateTime ExtractWeekStartFromString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return DateTime.Now;
            }
            var regex = new Regex(@"[\d]+");
            var matches = regex.Matches(dateString);
            if (matches.Count != 3) return DateTime.Now;
            var day = int.Parse(matches[0].Value);
            var month = int.Parse(matches[1].Value);
            var year = int.Parse(matches[2].Value);
            var dateTime = new DateTime(year, month, day);

            return dateTime;

            //Skonczyć regex i wyeksportować z niego pierwszą datę aby mogła byś ustawiona w propie dla WeekRange.

        }
    }
}
