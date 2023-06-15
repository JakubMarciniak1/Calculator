namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string firstValue;
        private string secondValue;
        private string previousTextBox2;

        private bool isResult = false;
        private bool isOperation = false;
        private bool isInput = false;
        private bool isNegated = false;

        private double textBox2Value;

        private RegularOperation currentOperation = RegularOperation.Null;
        private AdvanceOperation advanceOperation = AdvanceOperation.Null;


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void InputButtonClick(object sender, EventArgs e)
        {
            isInput = true;
            var inputValue = ((Button)sender).Text;

            if (isResult)
            {
                isResult = false;
                textBox1.Text = string.Empty;
            }
            if (textBox1.Text == "0" && inputValue != ",")
                textBox1.Text = string.Empty;
            if (textBox1.Text.Contains(",") && inputValue == ",")
                return;

            textBox1.Text += inputValue;

            if (textBox1.Text.StartsWith(","))
            {
                textBox1.Text = "0,";
                return;
            }

            if (currentOperation != RegularOperation.Null)
                secondValue += inputValue;

        }

        private void RevertLastInputComponent(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
                return;
            string revertedTextBoX = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            if (revertedTextBoX == string.Empty)
                isInput = false;
            textBox1.Text = revertedTextBoX;
            if (currentOperation != RegularOperation.Null)
                secondValue = (textBox1.Text);
        }

        private void NegateInputValue(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
                if (textBox1.Text == "0," || textBox1.Text == "0")
                    textBox1.Text = "0";
                else
                    textBox1.Text = (double.Parse(textBox1.Text) * (-1)).ToString();
            if (currentOperation != RegularOperation.Null)
                secondValue = (textBox1.Text);
            isNegated = true;
        }

        private void RegularOperationButtonClick(object sender, EventArgs e)
        {
            var operation = ((Button)sender).Text;

            if (textBox1.Text.EndsWith(","))
            {
                string revertedTextBoX = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                textBox1.Text = revertedTextBoX;
            }
       

            if (!isInput)
            {
                currentOperation = operation switch
                {
                    "+" => RegularOperation.Addition,
                    "-" => RegularOperation.Substraction,
                    "-/-" => RegularOperation.Division,
                    "x" => RegularOperation.Multiplication,
                    _ => RegularOperation.Null
                };

                textBox2.Text = previousTextBox2 + operation;

                if (textBox2.Text.StartsWith(operation) && !isNegated)
                    textBox2.Text = string.Empty;
            }
            else
            {
                isInput = false;
                if (isOperation)
                {
                    textBox2Value = PerformRegularOperation(currentOperation);
                    firstValue = textBox2Value.ToString();
                }
                else
                    firstValue = textBox1.Text;

                currentOperation = operation switch
                {
                    "+" => RegularOperation.Addition,
                    "-" => RegularOperation.Substraction,
                    "-/-" => RegularOperation.Division,
                    "x" => RegularOperation.Multiplication,
                    _ => RegularOperation.Null
                };

                previousTextBox2 = textBox2.Text + textBox1.Text;
                textBox2.Text += textBox1.Text + operation;
                textBox1.Text = string.Empty;
                isOperation = true;
            }
        }

        private void ResultButtonClick(object sender, EventArgs e)
        {
            if (firstValue == null || (isOperation && textBox1.Text == string.Empty))
                return;

            var result = PerformRegularOperation(currentOperation);
            textBox2.Text = string.Empty;
            textBox1.Text = result.ToString();
            currentOperation = RegularOperation.Null;
            secondValue = string.Empty;
            isOperation = false;
            isResult = true;
            isNegated = false;
    }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            firstValue = null;
            secondValue = null;
            previousTextBox2 = null;
            isResult = false;
            isOperation = false;
            isInput = false;
            isNegated = false;
            textBox2Value = 0;
            currentOperation = RegularOperation.Null;
            advanceOperation = AdvanceOperation.Null;

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void ClearFirstTextBoxButtonClick(object sender, EventArgs e)
        {

        }


        private double PerformRegularOperation(RegularOperation currentOperation)
        {
            var firstNumber = double.Parse(firstValue);
            var secondNumber = double.Parse(secondValue);
            var result = 0d;

            switch (currentOperation)
            {
                case RegularOperation.Null:
                    result = firstNumber;
                    break;
                case RegularOperation.Addition:
                    result = firstNumber + secondNumber;
                    break;
                case RegularOperation.Substraction:
                    result = firstNumber - secondNumber;
                    break;
                case RegularOperation.Multiplication:
                    result = firstNumber * secondNumber;
                    break;
                case RegularOperation.Division:
                    if (secondNumber == 0)
                    {
                        MessageBox.Show("You can't divide by 0");
                        break;
                    }
                    result = firstNumber / secondNumber;
                    break;
            }

            secondValue = string.Empty;

            return result;
        }

        private void AdvanceOperationButtonClick(object sender, EventArgs e)
        {
            var operation = (sender as Button).Text;

            if (textBox1.Text == string.Empty && isOperation)
                return;

            else
            {
                isInput = false;
                if (isOperation)
                {
                    textBox2Value = PerformRegularOperation(currentOperation);
                    firstValue = textBox2Value.ToString();
                }
                else
                    firstValue = textBox1.Text;

                advanceOperation = operation switch
                {
                    "√X" => AdvanceOperation.Root,
                    "X^2" => AdvanceOperation.Power,
                    "1/X" => AdvanceOperation.Percentage,
                    _ => AdvanceOperation.Null
                };

                previousTextBox2 = textBox2.Text + textBox1.Text;
                textBox2.Text += textBox1.Text + operation;
                textBox1.Text = string.Empty;
                isOperation = true;
            }
        }

        private double PerformAdvanceOperation(AdvanceOperation advanceOperation)
        {
            var firstNumber = double.Parse(firstValue);
            var result = 0d;

            switch (advanceOperation)
            {
                case AdvanceOperation.Power:
                    result = firstNumber * firstNumber;
                    break;
                case AdvanceOperation.Root:
                    result = Math.Sqrt(firstNumber);
                    break;
                case AdvanceOperation.Percentage:
                    result = 1 / firstNumber;
                    break;
                case AdvanceOperation.Null:
                    result = firstNumber;
                    break;
            }

            secondValue = string.Empty;

            return result;
        }


    }
}