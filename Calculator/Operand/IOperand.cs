using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operand
{
    public interface IOperand<T>
    {
        public T Apply(T a, T b);
    }
}
