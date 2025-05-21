using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        private double currentValue = 0;

        private string currentOperator = "";

        private bool isNewNumber = true;

        private string expression = "";

        public MainPage()
        {
            InitializeComponent();
            DisplayEntry.Text = "0"; 
        }

        private void NumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string input = button.Text;

            if (input == "," && DisplayEntry.Text.Contains(","))
                return;

            if (isNewNumber || DisplayEntry.Text == "0")
            {
                DisplayEntry.Text = input;
                isNewNumber = false;
            }
            else
            {
                DisplayEntry.Text += input;
            }

            expression += input;
        }

        
        private void OperatorClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string op = button.Text;

            if (!double.TryParse(DisplayEntry.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                return;

            
            if (currentOperator != "")
                Calculate(num);
            else
                currentValue = num;

            currentOperator = op;
            isNewNumber = true;

            expression += op;

            DisplayEntry.Text = expression;
        }

        private void EqualClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(DisplayEntry.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                return;

            Calculate(num);

            
            DisplayEntry.Text = currentValue.ToString("0.###", CultureInfo.CurrentCulture);

            
            expression = currentValue.ToString(CultureInfo.CurrentCulture);

            currentOperator = "";
            isNewNumber = true;
        }

     
        private void Calculate(double number)
        {
            switch (currentOperator)
            {
                case "+":
                    currentValue += number;
                    break;
                case "-":
                    currentValue -= number;
                    break;
                case "*":
                    currentValue *= number;
                    break;
                case "/":
                    if (number == 0)
                    {
                        DisplayEntry.Text = "Fel: division med 0";
                        currentValue = 0;
                        expression = "";
                        isNewNumber = true;
                        currentOperator = "";
                        return;
                    }
                    currentValue /= number;
                    break;
                default:
                    break;
            }
        }

      
        private void ClearClicked(object sender, EventArgs e)
        {
            currentValue = 0;
            currentOperator = "";
            expression = "";
            DisplayEntry.Text = "0";
            isNewNumber = true;
        }

        
        private void BackspaceClicked(object sender, EventArgs e)
        {
            if (DisplayEntry.Text.Length > 1)
            {
                DisplayEntry.Text = DisplayEntry.Text[..^1];
                expression = expression[..^1];
            }
            else
            {
                DisplayEntry.Text = "0";
                expression = "";
            }
        }
    }
}
