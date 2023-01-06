using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Exceptions
{
    public class SchedulerPlansEmptyException : Exception
    {
        public SchedulerPlansEmptyException()
        {

        }

        public SchedulerPlansEmptyException(string message) : base(message)
        {

        }
    }
}
