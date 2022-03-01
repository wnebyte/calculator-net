using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operand
{
    public class Multiplier : IOperand<BigInteger>
    {
        public BigInteger Apply(BigInteger a, BigInteger b)
        {
            return a * b;
        }
    }
}
