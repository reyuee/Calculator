using Microsoft.Maui.Controls;
using System;
using System.Globalization;

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
            DisplayLabel.Text = "0";
            ExpressionLabel.Text = "";
        }

        private void NumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string input = button.Text;

            if (input == "," && DisplayLabel.Text.Contains(","))
                return;

            if (isNewNumber || DisplayLabel.Text == "0")
            {
                DisplayLabel.Text = input;
                isNewNumber = false;
            }
            else
            {
                DisplayLabel.Text += input;
            }

            expression += input;
            ExpressionLabel.Text = expression;
        }

        private void OperatorClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string op = button.Text;

            if (!double.TryParse(DisplayLabel.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                return;

            if (currentOperator != "")
                Calculate(num);
            else
                currentValue = num;

            currentOperator = op;
            isNewNumber = true;
            expression += $" {op} ";
            ExpressionLabel.Text = expression;
        }

        private void EqualClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(DisplayLabel.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                return;

            Calculate(num);

            DisplayLabel.Text = currentValue.ToString("N0", CultureInfo.CurrentCulture);
            ExpressionLabel.Text = expression + " =";
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
                case "×":
                    currentValue *= number;
                    break;
                case "÷":
                    if (number == 0)
                    {
                        DisplayLabel.Text = "Fel: division med 0";
                        currentValue = 0;
                        expression = "";
                        isNewNumber = true;
                        currentOperator = "";
                        return;
                    }
                    currentValue /= number;
                    break;
            }
        }

        private void ClearClicked(object sender, EventArgs e)
        {
            currentValue = 0;
            currentOperator = "";
            expression = "";
            DisplayLabel.Text = "0";
            ExpressionLabel.Text = "";
            isNewNumber = true;
        }

        private void BackspaceClicked(object sender, EventArgs e)
        {
            if (DisplayLabel.Text.Length > 1)
            {
                DisplayLabel.Text = DisplayLabel.Text.Substring(0, DisplayLabel.Text.Length - 1);
                expression = expression.Length > 0 ? expression.Substring(0, expression.Length - 1) : "";
            }
            else
            {
                DisplayLabel.Text = "0";
                expression = "";
            }

            ExpressionLabel.Text = expression;
        }

        private void DecimalClicked(object sender, EventArgs e)
        {
            if (!DisplayLabel.Text.Contains(","))
            {
                DisplayLabel.Text += ",";
                expression += ",";
            }
        }

        private async void ShowNotImplementedPopup(object sender, EventArgs e)
        {
            await DisplayAlert("☝", "Will be implemented later on..", "OK");
        }
    }
}
