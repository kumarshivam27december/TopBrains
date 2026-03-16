using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public  class InvalidSeverityException : Exception
    {
        public InvalidSeverityException(string message) : base(message) { }
    }
}
