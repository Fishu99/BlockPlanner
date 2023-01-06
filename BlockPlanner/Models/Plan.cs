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
        public DateTime SelectedWeek { get; set; }
        public List<DayPlan> ScheduledDays { get; set; }

        public Plan()
        {
            
        }

        public Plan(string name, DateTime selectedWeek, List<DayPlan> scheduledDays)
        {
            Name = name;
            SelectedWeek = selectedWeek;
            ScheduledDays = scheduledDays;
        }

        public bool Conflicts(Plan newPlan)
        {
            var newPlanWeekDay = newPlan.SelectedWeek.DayOfWeek;
            if (SelectedWeek.DayOfWeek != newPlanWeekDay) return false;
            return Name == newPlan.Name;
        }
    }
}
