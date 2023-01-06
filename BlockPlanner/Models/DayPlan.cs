using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Models
{
    public class DayPlan
    {
        public WeekDay Day { get; }
        public List<Task> DayTasks { get; set; }

        public DayPlan(WeekDay planDay)
        {
            Day = planDay;
        }

    }
}
