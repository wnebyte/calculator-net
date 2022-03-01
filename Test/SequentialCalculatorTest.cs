using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System.Numerics;

namespace Test
{
    [TestClass]
    public class SequentialCalculatorTest
    {
		private ICalculator<BigInteger> calculator;

		[TestInitialize]
		public void Setup()
		{ 
			this.calculator = new SequentialCalculator(SequentialCalculator.Mode.REPLACE);
		}

        [TestMethod]
        public void Test00()
        {
			BigInteger value = calculator.Calculate("2 + 2");
			Assert.AreEqual(4, value);
        }

		[TestMethod]
		public void Test01()
		{
			BigInteger value = calculator.Calculate("2 * 10 + 500");
			Assert.AreEqual(520, value);
		}

		[TestMethod]
		public void Test02()
		{
			BigInteger value = calculator.Calculate("1000");
			Assert.AreEqual(1000, value);
		}

		[TestMethod]
		public void TestEvalException00()
		{
			Assert.ThrowsException<EvalException>(() => calculator.Calculate(""));
		}

		[TestMethod]
		public void TestEvalException01()
		{
			Assert.ThrowsException<EvalException>(() => calculator.Calculate(null));
		}

		[TestMethod]
		public void TestEvalException02()
		{
			Assert.ThrowsException<EvalException>(() => calculator.Calculate("2 2 *"));
		}

		[TestMethod]
		public void TestEvalException03()
		{
			Assert.ThrowsException<EvalException>(() => calculator.Calculate("2 / 0"));
		}

		[TestMethod]
		public void TestEvalException04()
		{
			Assert.ThrowsException<EvalException>(() => calculator.Calculate("2 * * 2"));
		}

		[TestMethod]
		public void TestEvalException05()
		{
			Assert.ThrowsException<EvalException>(() => calculator.Calculate("* 2 * 22"));
		}
	}
}
