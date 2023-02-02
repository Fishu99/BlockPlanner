using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Exceptions
{
    public class UnrecognizedWeekDayException : Exception
    {
        public UnrecognizedWeekDayException() {}

        public UnrecognizedWeekDayException(string message) : base(message) { }
    }
}
