using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class EvalException : Exception
    {
        public EvalException() : base() { }

        public EvalException(string msg) : base(msg) { }

        public EvalException(string msg, Exception inner) : base(msg, inner) { }

    }
}
