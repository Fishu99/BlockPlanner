using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Exceptions
{
    public class InvalidSizeException : Exception
    {
        public InvalidSizeException() { }
        public InvalidSizeException(string message) : base(message) { }

    }
}
