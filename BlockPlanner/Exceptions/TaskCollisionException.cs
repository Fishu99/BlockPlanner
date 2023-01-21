using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Exceptions
{
    public class TaskCollisionException : Exception
    {
        public TaskCollisionException() {}
        public TaskCollisionException(string message) : base(message) {}
        public TaskCollisionException(string message, Exception innerException) : base(message, innerException) {}

    }
}
