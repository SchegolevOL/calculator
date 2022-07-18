using System;
using System.Collections.Generic;
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
        private string[] partsExpression;
        private List<string> ListPartsExpression;
        private bool _flagPoint = false;
        private bool _flagFirstZero = false;
        private string _result;
        public MainWindow()
        {
            ListPartsExpression = new List<string>();
            InitializeComponent();
        }


        private void ButtonClickNumber(object sender, RoutedEventArgs e)
        {
            if (!_flagFirstZero)
            {
                _input += (sender as Button)?.Content;
                Input.Text = _input;
                _flagFirstZero = false;
            }
            if (_input == "0") _flagFirstZero = true;
            if (_input[_input.Length - 1] == ' ' && (string?)((sender as Button)?.Content) == "0") _flagFirstZero = true;
        }
        private void ButtonClickPoint(object sender, RoutedEventArgs e)
        {
            if (_input?[_input.Length - 1] == ' ' || _flagPoint == true) return;
            _flagPoint = true;
            _flagFirstZero = false;
            _input += (sender as Button)?.Content;
            Input.Text = _input;
        }
        private void ButtonClickAction(object sender, RoutedEventArgs e)
        {
            if (_input?[_input.Length - 1] == ' ' || _input == null) return;
            _input += (" " + (sender as Button)?.Content) + " ";
            _flagPoint = false;
            _flagFirstZero = false;
            Input.Text = _input;
        }
        private void ButtonClickEnter(object sender, RoutedEventArgs e)
        {
            int index;
            if (_input?[_input.Length - 1] == ' ' || _input == null) return;
            partsExpression = _input.Split(' ');

            for (int i = 0; i < partsExpression.Length; i++)
            {
                ListPartsExpression.Add(partsExpression[i]);
            }
            do
            {
                index = ListPartsExpression.IndexOf("*");
                if (index != -1)
                {
                    MultiplicationByIndex(index);
                }
            } while (index != -1);
            do
            {
                index = ListPartsExpression.IndexOf("/");
                if (index != -1)
                {
                    DivisionByIndex(index);
                }
            } while (index != -1);
            do
            {
                index = ListPartsExpression.IndexOf("+");
                if (index != -1)
                {
                    SumByIndex(index);
                }
            } while (index != -1);
            do
            {
                index = ListPartsExpression.IndexOf("-");
                if (index != -1)
                {
                    DifferenceByIndex(index);
                }
            } while (index != -1);



            _result = ListPartsExpression[0].ToString();
            OutResult.Text = _result;
            _input = null;
            Input.Text = "0";
            _flagPoint = false;
            _flagFirstZero = false;
            ListPartsExpression.Clear();
        }

        private void ButtonClickDel(object sender, RoutedEventArgs e)
        {
            int count = 1;
            if (_input == null) return;
            if (_input[_input.Length - 1] == ',')
            {
                _flagPoint = false;
               
            }
            if (_input[_input.Length - 1] == ' ')
            {
                count = 3;
                _flagFirstZero = false;
            }

            
            _input = _input.Remove(_input.Length - count, count);

            if (_input.Length == 0)
            {
                _input = null;
                _flagPoint = false;
                _flagFirstZero = false;
            }
            

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
            _flagPoint = false;
            _flagFirstZero = false;
            Print();
        }
        private void ButtonClickC(object sender, RoutedEventArgs e)
        {
            if (_input == null) return;
            while (_input.Length != 0)
            {
                _input = _input.Remove(_input.Length - 1, 1);
            }
            _flagPoint = false;
            _flagFirstZero = false;
            Print();
        }
        private void Print()
        {
            if (_input.Length == 0) _input = null;
            if (_input == null) Input.Text = "0";
            else Input.Text = _input;
        }
        private void MultiplicationByIndex(int index)
        {
            double result = double.Parse(ListPartsExpression[index - 1]) * double.Parse(ListPartsExpression[index + 1]);
            ListPartsExpression[index] = result.ToString();
            ListPartsExpression.RemoveAt(index + 1);
            ListPartsExpression.RemoveAt(index - 1);
        }
        private void DivisionByIndex(int index)
        {
            double result = double.Parse(ListPartsExpression[index - 1]) / double.Parse(ListPartsExpression[index + 1]);
            ListPartsExpression[index] = result.ToString();
            ListPartsExpression.RemoveAt(index + 1);
            ListPartsExpression.RemoveAt(index - 1);
        }
        private void DifferenceByIndex(int index)
        {
            double result = double.Parse(ListPartsExpression[index - 1]) - double.Parse(ListPartsExpression[index + 1]);
            ListPartsExpression[index] = result.ToString();
            ListPartsExpression.RemoveAt(index + 1);
            ListPartsExpression.RemoveAt(index - 1);
        }
        private void SumByIndex(int index)
        {
            double result = double.Parse(ListPartsExpression[index - 1]) + double.Parse(ListPartsExpression[index + 1]);
            ListPartsExpression[index] = result.ToString();
            ListPartsExpression.RemoveAt(index + 1);
            ListPartsExpression.RemoveAt(index - 1);
        }
    }
}
