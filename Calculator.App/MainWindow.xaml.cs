using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.App
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string? _inputSrt = null;
        public string InputStr
        {
            get => _inputSrt;
            set
            {
                if (value is null) return;
                if (value == _inputSrt) return;
                _inputSrt = value;
                OnPropertyChanged(nameof(InputStr));
            }
        }

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
                _inputSrt += (sender as Button)?.Content;
                Input.Text = _inputSrt;
                _flagFirstZero = false;
            }
            if (_inputSrt == "0") _flagFirstZero = true;
            if (_inputSrt[_inputSrt.Length - 1] == ' ' && (string?)((sender as Button)?.Content) == "0") _flagFirstZero = true;
        }
        private void ButtonClickPoint(object sender, RoutedEventArgs e)
        {
            if (_inputSrt?[_inputSrt.Length - 1] == ' ' || _flagPoint == true) return;
            _flagPoint = true;
            _flagFirstZero = false;
            _inputSrt += (sender as Button)?.Content;
            Input.Text = _inputSrt;
        }
        private void ButtonClickAction(object sender, RoutedEventArgs e)
        {
            if (_inputSrt?[_inputSrt.Length - 1] == ' ' || _inputSrt == null) return;
            _inputSrt += (" " + (sender as Button)?.Content) + " ";
            _flagPoint = false;
            _flagFirstZero = false;
            Input.Text = _inputSrt;
        }
        private void ButtonClickEnter(object sender, RoutedEventArgs e)
        {
            int index;
            if (_inputSrt?[_inputSrt.Length - 1] == ' ' || _inputSrt == null) return;
            partsExpression = _inputSrt.Split(' ');

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
            _inputSrt = null;
            Input.Text = "0";
            _flagPoint = false;
            _flagFirstZero = false;
            ListPartsExpression.Clear();
        }

        private void ButtonClickDel(object sender, RoutedEventArgs e)
        {
            int count = 1;
            if (_inputSrt == null) return;
            if (_inputSrt[_inputSrt.Length - 1] == ',')
            {
                _flagPoint = false;
               
            }
            if (_inputSrt[_inputSrt.Length - 1] == ' ')
            {
                count = 3;
                _flagFirstZero = false;
            }

            
            _inputSrt = _inputSrt.Remove(_inputSrt.Length - count, count);

            if (_inputSrt.Length == 0)
            {
                _inputSrt = null;
                _flagPoint = false;
                _flagFirstZero = false;
            }
            

            if (_inputSrt == null) Input.Text = "0";
            else Input.Text = _inputSrt;
        }
        private void ButtonClickCE(object sender, RoutedEventArgs e)
        {
            if (_inputSrt == null) return;
            while (_inputSrt.Length != 0)
            {
                if (_inputSrt[_inputSrt.Length - 1] == ' ') break;
                _inputSrt = _inputSrt.Remove(_inputSrt.Length - 1, 1);
            }
            _flagPoint = false;
            _flagFirstZero = false;
            
        }
        private void ButtonClickC(object sender, RoutedEventArgs e)
        {
            if (_inputSrt == null) return;
            while (_inputSrt.Length != 0)
            {
                _inputSrt = _inputSrt.Remove(_inputSrt.Length - 1, 1);
            }
            _flagPoint = false;
            _flagFirstZero = false;
            
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
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
