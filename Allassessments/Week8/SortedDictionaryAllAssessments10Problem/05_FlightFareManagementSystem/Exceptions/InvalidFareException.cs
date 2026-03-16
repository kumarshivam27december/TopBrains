using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class InvalidFareException : Exception
{
        public InvalidFareException(string message) : base(message) { }
}
}
