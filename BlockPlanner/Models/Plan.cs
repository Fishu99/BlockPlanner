using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Models
{
    public class Plan
    {
        public string Name { get; set; }
        public DateTime WeekStartTime { get; set; }
        public DateTime WeekEndTime { get; set; }
        public List<DayPlan> ScheduledDays { get; set; }

        public Plan()
        {
            
        }

        public Plan(string name, DateTime weekStartTime, DateTime weekEndTime, List<DayPlan> scheduledDays)
        {
            Name = name;
            WeekStartTime = weekStartTime;
            WeekEndTime = weekEndTime;
            ScheduledDays = scheduledDays;
        }

        public bool Conflicts(Plan newPlan)
        {
            var newPlanWeekDay = newPlan.WeekStartTime.DayOfWeek;
            if (WeekStartTime.DayOfWeek != newPlanWeekDay) return false;
            return Name == newPlan.Name;
        }
    }
}
