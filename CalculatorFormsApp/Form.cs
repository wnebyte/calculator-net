using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator;
using Calculator.Util;

namespace CalculatorFormsApp
{
	/*
	* The underlying calculator component is capable of cumputing negative numbers, 
	* but can not handle negative numbers as input, 
	* therefore this calculator form displays any negative results computed only through its ResultLabel, 
	* and not through its ExpressionLabel where it could be used as further input. 
	* Furthermore, expressions are parsed from left-to-right with no priority given to any operands.  
	*/
	public partial class Form : System.Windows.Forms.Form
    {
		// operand constants
		private static readonly IList<char> opChars = new List<char>()
		{
			{ '+' },
			{ '-' },
			{ '*' },
			{ '/' },
		};

		// numerical & operand buttons
		private readonly Button[] buttons;

		// calculator component
		private readonly ICalculator<BigInteger> calculator;

        public Form()
        {
            InitializeComponent();
			this.buttons = new Button[14]
			{
				// numerical buttons
				this.ButtonZero,
				this.ButtonOne,
				this.ButtonTwo,
				this.ButtonThree,
				this.ButtonFour,
				this.ButtonFive,
				this.ButtonSix,
				this.ButtonSeven,
				this.ButtonEight,
				this.ButtonNine,
				// operand buttons
				this.ButtonAddition,
				this.ButtonSubtraction,
				this.ButtonMultiplication,
				this.ButtonDivision
			};

			foreach (Button btn in buttons)
			{
				btn.Click += delegate (object sender, EventArgs e)
				{
					this.Button_Click(sender, e, btn.Text.ToCharArray()[0]);
				};
			}

			this.calculator = new SequentialCalculator(SequentialCalculator.Mode.STRICT);
        }

		private void Button_Click(object sender, EventArgs e, char c)
		{
			String text = String.Join("", ExpressionBox.Text);
			char[] array = text.ToCharArray();
			// the expression is currently empty
			if (array.Length == 0)
			{
				// the specified char is an operand
				if (opChars.Contains(c))
				{
					return;
				}
			}
			// the expression is currently not empty
			else
			{
				char last = array[array.Length - 1];
				// the specified char is an operand, and the last char in the expression is also an operand
				if (opChars.Contains(c) && opChars.Contains(last))
				{
					return;
				}
			}
			text = text + c;
			ExpressionBox.Text = text;
		}

		private void ButtonEquals_Click(object sender, EventArgs e)
		{
			try
			{
				string exp = String.Join("", ExpressionBox.Text);
				BigInteger value = calculator.Calculate(exp);
				ResultLabel.Text = value.ToString("R");
				if (value < 0)
				{
					value = 0;
				}
				ExpressionBox.Text = value.ToString("R");
			}
			catch (EvalException ex)
			{
				ExpressionBox.Text = String.Empty;
				MessageBox.Show(ex.Message);
			}
		}

		private void ButtonClear_Click(object sender, EventArgs e)
		{
			ExpressionBox.Text = String.Empty;
			ResultLabel.Text = String.Empty;
		}
	}
}
