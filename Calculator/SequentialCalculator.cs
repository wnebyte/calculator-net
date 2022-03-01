using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Calculator.Operand;
using Calculator.Struct;
using Calculator.Util;
using Calculator.Util.Func;

namespace Calculator
{
    /**
     * This class represents a simple calculator capable of calculating sequential numerical expressions.  
     */
    public class SequentialCalculator : ICalculator<BigInteger>
    {

        public enum Mode
        { 
            REPLACE,
            STRICT
        }

		/*
        ###########################
        #      STATIC FIELDS      #
        ###########################
        */

        private static readonly IList<char> legal = new List<char>()
        {
            '+',
            '-',
            '*',
            '/',
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            '(',
            ')',
            ' '
        };

        private static readonly IList<char> numbers = new List<char>()
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9'
        };

        private static readonly IDictionary<string, IOperand<BigInteger>> ops = new Dictionary<string, IOperand<BigInteger>>()
        {
            { "+", new Adder() },
            { "-", new Subtracter() },
            { "*", new Multiplier() },
            { "/", new Divider() }
        };

		/*
        ###########################
        #         FIELDS          #
        ###########################
        */

		private readonly ITransformationChain<char[]> cchain = new TransformationChain<char[]>();

        private readonly ITransformationChain<TokenSequence> tchain = new TransformationChain<TokenSequence>();

        /*
        ###########################
        #       CONSTRUCTORS      #
        ###########################
        */

        public SequentialCalculator() : this(Mode.REPLACE) { }

        public SequentialCalculator(Mode mode)
        {
            cchain.Add((c) =>
            {
                // illegal characters are removed or replaced.
                StringBuilder builder = new();

                for (int i = 0; i < c.Length; i++)
                {
                    char a = c[i];

                    if (legal.Contains(a))
                    {
                        builder.Append(a);
                    }
                    else if (mode == Mode.STRICT)
                    {
                        throw new EvalException(
                            String.Format(
                                "Illegal char: '{0}' at index: '{1}'.", a, i
                                ));
                    }
                }

                return builder.ToString().ToCharArray();
            });
            tchain.Add((t) =>
            {
                // whitespace tokens should now be removed.
                t.RemoveIf((s) => s == " ");
                t.RemoveIf((s) => s == "(");
                t.RemoveIf((s) => s == ")");
                return t;
            });
        }
        
        /*
        ###########################
        #          METHODS        #
        ###########################
        */

        public BigInteger Calculate(string exp)
        {
            if (exp.IsNullOrEmpty())
            {
                throw new EvalException(
                    String.Format(
                        "Expression can not be evaluated."
                        ));
            }

            char[] array = cchain.Transform(exp.ToCharArray());
            TokenSequence tokens = tchain.Transform(TokenSequence.Tokenize(array));
            TokenSequence numTokens = tokens.TakeWhileIndex((i) => i % 2 == 0);
            TokenSequence opTokens = tokens.TakeWhileIndex((i) => i % 2 != 0);

            // expression needs to contain one more numerical value than operand
            if (numTokens.Count() != opTokens.Count() + 1)
            {
                throw new EvalException(
                    String.Format(
                        "Expression can not be evaluated."
                        ));
            }

            // every token at an even indice
            foreach (string token in numTokens)
            {
                // every character containied within the token needs to be a numerical character
                if (!token.All((c) => numbers.Contains(c)))
                {
                    throw new EvalException(
                        String.Format(
                            "Token: '{0}' is not a numerical value.", token
                            ));
                }
            }

            // every token at an uneven indice
            foreach (string token in opTokens)
            {
                // token needs to correspond to a known operand character (+, -, *, /)
                if (!ops.Keys.Contains(token))
                {
                    throw new EvalException(
                        String.Format(
                            "Token: '{0}' is not an operand value.", token
                            ));
                }
            }

			BigInteger value = BigInteger.Parse(numTokens.TakeFirst());

			while (numTokens.IsNotEmpty())
			{
				BigInteger num = BigInteger.Parse(numTokens.TakeFirst());

				if (opTokens.IsNotEmpty())
				{
					string op = opTokens.TakeFirst();

					IOperand<BigInteger> operand = ops[op];

					value = operand.Apply(value, num);
				}
			}

			return value;
        }
    }
}
