using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BlockPlanner.Models;

namespace BlockPlanner.Exceptions
{
    public class SchedulePlanAlreadyExistsException : Exception
    {
        public Plan ExistingPlan { get; }
        public Plan IncomingPlan { get; }

        public SchedulePlanAlreadyExistsException(Plan existingPlan, Plan incomingPlan)
        {
            ExistingPlan = existingPlan;
            IncomingPlan = incomingPlan;
        }

        public SchedulePlanAlreadyExistsException(string message, Plan existingPlan, Plan incomingPlan) : base(message)
        {
            ExistingPlan = existingPlan;
            IncomingPlan = incomingPlan;
        }

        public SchedulePlanAlreadyExistsException(string message, Exception innerException, Plan existingPlan, Plan incomingPlan) : base(message, innerException)
        {
            ExistingPlan = existingPlan;
            IncomingPlan = incomingPlan;
        }
    }
}