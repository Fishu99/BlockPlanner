using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockPlanner.Exceptions
{
    public class ColorConversionFailedException : Exception
    {
        public ColorConversionFailedException()
        {

        }

        public ColorConversionFailedException(string message) : base(message)
        {

        }

        public ColorConversionFailedException(string message, Exception innerException) : base()
        {

        }
    }
}