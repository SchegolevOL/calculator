using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? _input = null;
        private bool FlagPoint = false;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonClickNumber(object sender, RoutedEventArgs e)
        {
            if ((string?)((sender as Button)?.Content) == "0" && _input == null) return;
            _input += (sender as Button)?.Content;
            Input.Text = _input;
        }
        private void ButtonClickPoint(object sender, RoutedEventArgs e)
        {
            if (_input?[_input.Length - 1] == ' ' || FlagPoint == true || _input == null) return;
            FlagPoint = true;
            _input += (sender as Button)?.Content;
            Input.Text = _input;
        }
        private void ButtonAction(object sender, RoutedEventArgs e)
        {
            if (_input?[_input.Length - 1] == ' '|| _input==null) return;
            _input += (" " + (sender as Button)?.Content) + " ";
            FlagPoint = false;
            Input.Text = _input;
        }

    }
}
