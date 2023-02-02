using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Exceptions;

namespace BlockPlanner.Models
{
    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public static class WeekDayMethods
    {
        public static WeekDay TryGetWeekDay(string day)
        {
            try
            {
                switch (day)
                {
                    case ("Mon"):
                        return WeekDay.Monday;
                    case ("Tue"):
                        return WeekDay.Tuesday;
                    case ("Wed"):
                        return WeekDay.Wednesday;
                    case ("Thu"):
                        return WeekDay.Thursday;
                    case ("Fri"):
                        return WeekDay.Friday;
                    case ("Sat"):
                        return WeekDay.Saturday;
                    case ("Sun"):
                        return WeekDay.Sunday;
                    default:
                        throw new UnrecognizedWeekDayException(day);
                }
            }
            catch (UnrecognizedWeekDayException ex)
            {
                Console.WriteLine("Parse operation failed, unrecognized element: " + ex.Message);
                return WeekDay.Monday;
            }
        }

        public static int GetId(this WeekDay day)
        {
            return (int)day;
        }

        public static int GetElementsCount()
        {
            return Enum.GetNames(typeof(WeekDay)).Length;
        }
    }
}
