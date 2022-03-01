using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operand
{
    public class Divider : IOperand<BigInteger>
    {
        public BigInteger Apply(BigInteger a, BigInteger b)
        {
			if (b == 0)
			{
				throw new EvalException(
					String.Format(
						"Denominator: '{0}' must not be 0.", b
						));
			}
			return a / b;
        }
    }
}
