using System.Windows;
using System.Windows.Controls;

namespace Calculator.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? _input = null;
        private bool _flagPoint = false;
        private string _result;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonClickNumber(object sender, RoutedEventArgs e)
        {
            if ((string?)((sender as Button)?.Content) == "0" && (_input == null || _input[_input.Length - 1] == ' ')) return;
            _input += (sender as Button)?.Content;
            Input.Text = _input;
        }
        private void ButtonClickPoint(object sender, RoutedEventArgs e)
        {
            if (_input?[_input.Length - 1] == ' ' || _flagPoint == true) return;
            if (_input == null) _input += "0";
            
            _flagPoint = true;
            _input += (sender as Button)?.Content;
            Input.Text = _input;
        }
        private void ButtonClickAction(object sender, RoutedEventArgs e)
        {
            if (_input?[_input.Length - 1] == ' ' || _input == null) return;
            _input += (" " + (sender as Button)?.Content) + " ";
            _flagPoint = false;
            Input.Text = _input;
        }
        private void ButtonClickEnter(object sender, RoutedEventArgs e)
        {
            double value = 0;
            if (_input?[_input.Length - 1] == ' ' || _input == null) return;
            string[] partsExpression = _input.Split(' ');
            for (int i = 1; i < partsExpression.Length; i += 2)
            {
                switch (partsExpression[i])
                {
                    case "+":
                        value = double.Parse(partsExpression[i - 1]) + double.Parse(partsExpression[i + 1]) + 0.00000001;
                        break;
                    case "-":
                        value = double.Parse(partsExpression[i - 1]) - double.Parse(partsExpression[i + 1]);
                        break;
                    case "*":
                        value = double.Parse(partsExpression[i - 1]) * double.Parse(partsExpression[i + 1]);
                        break;
                    case "/":
                        value = double.Parse(partsExpression[i - 1]) / double.Parse(partsExpression[i + 1]);
                        break;
                    default:
                        break;
                }
            }
            _result = value.ToString();
            OutResult.Text = _result;
            _input = null;
            Input.Text = "0";
            _flagPoint = false;
        }

        private void ButtonClickDel(object sender, RoutedEventArgs e)
        {
            int count = 1;
            if (_input == null) return;
            if (_input[_input.Length - 1] == ',')
            {
                _flagPoint = false;
                return;
            }
            if (_input[_input.Length - 1] == ' ') count = 3;
            _input = _input.Remove(_input.Length - count, count);

            if (_input.Length == 0) _input = null;

            if (_input == null) Input.Text = "0";
            else Input.Text = _input;
        }
        private void ButtonClickCE(object sender, RoutedEventArgs e)
        {
            if (_input == null) return;
            while (_input.Length != 0)
            {
                if (_input[_input.Length - 1] == ' ') break;
                _input = _input.Remove(_input.Length - 1, 1);
            }
            if (_input.Length == 0) _input = null;
            if (_input == null) Input.Text = "0";
            else Input.Text = _input;
        }
        private void ButtonClickC(object sender, RoutedEventArgs e)
        {
            if (_input == null) return;
            while (_input.Length != 0)
            {
                _input = _input.Remove(_input.Length - 1, 1);
            }
            if (_input.Length == 0) _input = null;
            if (_input == null) Input.Text = "0";
            else Input.Text = _input;
        }
    }
}
