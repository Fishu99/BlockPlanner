using BlockPlanner.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Models
{
    public class DayPlan
    {
        private List<Task> _dayTasks;
        public WeekDay Day { get; }
        public List<Task> DayTasks
        {
            get => _dayTasks;
            set => _dayTasks = value;
        }

        public DayPlan(WeekDay planDay)
        {
            Day = planDay;
            DayTasks = new List<Task>();
        }

        public void AddNewTask(Task task, out int placementId)
        {
            placementId = 0;
            if (_dayTasks != null)
            {
                foreach (var existingTask in _dayTasks)
                {
                    if (Task.IsScheduledBetween(existingTask, task))
                    {
                        throw new TaskCollisionException();
                    }

                    if (task.StartTime > existingTask.EndTime)
                    {
                        placementId++;
                    }
                }

                _dayTasks.Insert(placementId, task);
            }
        }
    }
}
