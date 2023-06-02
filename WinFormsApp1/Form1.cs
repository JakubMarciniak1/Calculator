namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string firstValue;
        private string secondValue;
        private bool isResult = false;
        private bool isOperation = false;
        private double textBox2Value;

        private RegularOperation currentOperation = RegularOperation.Null;


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void InputButtonClick(object sender, EventArgs e)
        {
            var inputValue = (sender as Button).Text;

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

        private void RevertInputValue(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                textBox1.Text = (double.Parse(textBox1.Text) * (-1)).ToString();
                //if (firstValue != null)

                    
            }
        }

        private void RegularOperationButtonClick(object sender, EventArgs e)
        {
            if (isOperation)
            {
                textBox2Value = PerformRegularOperation(currentOperation);
                firstValue = textBox2Value.ToString();
            }
            else
                firstValue = textBox1.Text;

            var operation = (sender as Button).Text;

            currentOperation = operation switch
            {
                "+" => RegularOperation.Addition,
                "-" => RegularOperation.Substraction,
                "-/-" => RegularOperation.Division,
                "x" => RegularOperation.Multiplication,
                _ => RegularOperation.Null
            };


            textBox2.Text += textBox1.Text + operation;
            textBox1.Text = string.Empty;
            isOperation = true;

        }

        private void ResultButtonClick(object sender, EventArgs e)
        {
            if (firstValue == null)
                return;

            var result = PerformRegularOperation(currentOperation);
            textBox2.Text = string.Empty;
            textBox1.Text = result.ToString();
            currentOperation = RegularOperation.Null;
            secondValue = string.Empty;
            isOperation = false;
            isResult = true;
        }

        private void ClearButtonClick(object sender, EventArgs e)
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


    }
}