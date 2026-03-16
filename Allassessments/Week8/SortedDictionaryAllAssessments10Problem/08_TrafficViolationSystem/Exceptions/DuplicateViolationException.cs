using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DuplicateViolationException : Exception
    {
        public DuplicateViolationException(string message) : base(message) { }
    }
}
