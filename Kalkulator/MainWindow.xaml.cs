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
using System.Text.RegularExpressions;
namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string[] Operations2 = new string[4] { "+", "-", "*", "/" };
        private bool IsLastButtonEq = false;
        private double Calculate(double x, string operation,double y)
        {
            switch (operation)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return x / y;
                default:
                    return x % y;
            }
        }
        private void ClearMemoryTextField()
        {
            if (IsLastButtonEq)
            {
                MemoryTextField.Text = "";
                IsLastButtonEq = false;
            }
            
        }
        private void ClearCurrentTextField()
        {
            if (IsLastButtonEq)
            {
                CurrentNumberTextField.Text = "";
                
            }

        }
        private void Numeric_Click(object sender, RoutedEventArgs e)
        {
            ClearCurrentTextField();
            ClearMemoryTextField();
            Button button = (Button)sender;
            CurrentNumberTextField.Text = CurrentNumberTextField.Text + button.Content;
        }

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            ClearCurrentTextField();
            ClearMemoryTextField();
            if (CurrentNumberTextField.Text!="0")
            {
                Button button = (Button)sender;
                CurrentNumberTextField.Text = CurrentNumberTextField.Text+ button.Content.ToString();
            }
        }

        private void Plus_Minus_Click(object sender, RoutedEventArgs e)
        {
            ClearMemoryTextField();
            if (Regex.Match(CurrentNumberTextField.Text, @"^\d+\.?\d*").Success)
            {
                CurrentNumberTextField.Text = "-" + CurrentNumberTextField.Text;
            }
            else if (Regex.Match(CurrentNumberTextField.Text, @"^-\d+\.?\d*").Success)
            {
                CurrentNumberTextField.Text =  CurrentNumberTextField.Text.Substring(1);
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            

            if (MemoryTextField.Text != "" && CurrentNumberTextField.Text != "")
            {
                MemoryTextField.Text += CurrentNumberTextField.Text;
                string[] values = MemoryTextField.Text.Split();
                List<double> numbers = new List<double>();
                List<string> operations = new List<string>();
                for (int i = 0; i < values.Length; i+=2)
                {
                    numbers.Add(Convert.ToDouble(values[i]));
                }
                for (int i = 1; i < values.Length; i+=2)
                {
                    operations.Add(values[i]);
                }
                for (int i = 0; i < operations.Count; i++)
                {
                    numbers[i + 1] = Calculate(numbers[i], operations[i], numbers[i + 1]);
                }
                CurrentNumberTextField.Text = numbers[numbers.Count - 1].ToString();
                IsLastButtonEq = true;
                
            }
        }

        private void Function2_Click(object sender, RoutedEventArgs e)
        {
            ClearMemoryTextField();
            Button button = (Button)sender;
            MemoryTextField.Text += CurrentNumberTextField.Text + " " + button.Content + " " ;
            CurrentNumberTextField.Text = "";
        }

        private void Function1_Click(object sender, RoutedEventArgs e)
        {
            ClearMemoryTextField();
            Button button = (Button)sender;
            string operation = button.Content.ToString();
            Console.WriteLine(operation);
            if (CurrentNumberTextField.Text != "")
            {
                switch (operation)
                {
                    case "x^2":
                        CurrentNumberTextField.Text = Math.Pow(Double.Parse(CurrentNumberTextField.Text), 2).ToString();
                        break;
                    case "1/x":
                        double x = Double.Parse(CurrentNumberTextField.Text);
                        //if (x==0)
                        //{
                        //    CurrentNumberTextField.Text = "Dzielenie przez 0!!";
                        //}
                        //else
                        {
                            CurrentNumberTextField.Text = (1 / x).ToString();
                        }
                        break;
                    case "SQRT(x)":
                        double y = Double.Parse(CurrentNumberTextField.Text);
                        if (y >= 0)
                        {
                            CurrentNumberTextField.Text = Math.Sqrt(y).ToString();
                        }
                        else
                        {
                            CurrentNumberTextField.Text = "Niepoprawne wejście";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumberTextField.Text = "";
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumberTextField.Text = "";
            MemoryTextField.Text = "";
        }

       
        private void DEL_Click(object sender, RoutedEventArgs e)
        {
            string number = CurrentNumberTextField.Text;
            if (number.Length>1)
            {
                CurrentNumberTextField.Text = number.Substring(0, number.Length - 1);
            }
            else
            {
                CurrentNumberTextField.Text = "";
            }
            
        }

        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            int n = CurrentNumberTextField.Text.Length;
            if (n > 0)
            {
                string reg = $"{n}";
                if (Regex.Match(CurrentNumberTextField.Text, @"\d{" + reg + "}").Success)
                {
                    CurrentNumberTextField.Text += ",";
                }
            }
        }
    }
}
