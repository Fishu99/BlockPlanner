using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Exceptions;

namespace BlockPlanner.Models
{
    public class Scheduler
    {
        private readonly List<Plan> _plans;

        public List<Plan> Plans => _plans;

        public Scheduler()
        {
            _plans = new List<Plan>();
        }

        public void AddNewPlan(Plan newPlan)
        {
            if (_plans != null)
            {
                foreach (var exisitingPlan in _plans.Where(exisitingPlan => exisitingPlan.Conflicts(newPlan)))
                {
                    throw new SchedulePlanAlreadyExistsException(
                        "There is a plan in scheduler that has the same date and name like current one.",
                        exisitingPlan, 
                        newPlan);
                }

                _plans.Add(newPlan);
            }
            else
            {
                throw new SchedulerPlansEmptyException();
            }
        }

    }
}
