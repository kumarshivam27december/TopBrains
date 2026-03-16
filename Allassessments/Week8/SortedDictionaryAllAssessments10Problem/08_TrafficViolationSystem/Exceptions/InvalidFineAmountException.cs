using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class InvalidFineAmountException : Exception
    {
        public InvalidFineAmountException(string message): base(message) { }
    }
}
