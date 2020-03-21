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
using System.Globalization;
using System.Threading;

namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            char DecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            //
            InitializeComponent();
            b_52.Content = DecimalSeparator;
        }
        //flagi do ochrony przed błędnymi obliczeniami
        private bool IsLastButtonEquals = false;
        private bool IsLastButton1ArgFunction = false;
        //do globalizacji
        char DecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        //pomocnicze metody do czyszczenia okien
        private void ClearMemoryTextField()
        {
            if (IsLastButtonEquals)
            {
                MemoryTextField.Text = "";
                IsLastButtonEquals = false;
            }
            
        }
        private void ClearCurrentTextField()
        {
            if (IsLastButtonEquals)
            {
                CurrentNumberTextField.Text = "";
                
            }

        }
        //metody do wpisywania liczb i symboli operacji do pól tekstowych
        private void Write_Number(string number)
        {
            if (!IsLastButton1ArgFunction)
            {
                ClearCurrentTextField();
                ClearMemoryTextField();
                if (number != "0")
                {
                    CurrentNumberTextField.Text += number;
                }
                else if (CurrentNumberTextField.Text != "0")
                {
                    CurrentNumberTextField.Text += number;
                }
            }
        }

        private void Write_Operation(string operation)
        {
            if (Regex.Match(CurrentNumberTextField.Text, @"\d+$").Success)
            {
                ClearMemoryTextField();
                
                MemoryTextField.Text += $"{CurrentNumberTextField.Text} {operation} ";
                CurrentNumberTextField.Text = "";
                IsLastButton1ArgFunction = false;
            }

        }

        //Metody do klikania na odpowiednie przyciski

        private void Numeric_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            
                Write_Number(button.Content.ToString());
            
            
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
            IsLastButton1ArgFunction = true;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            
           if (MemoryTextField.Text != "" && CurrentNumberTextField.Text != ""&&!IsLastButtonEquals)
            {
                MemoryTextField.Text += CurrentNumberTextField.Text;
                CurrentNumberTextField.Text = MathematicalBackground.PerformCalculations(MemoryTextField.Text).ToString();
                IsLastButtonEquals = true;
                
            }
        }

        private void Function_2_Arguments_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = (Button)sender;
            Write_Operation(button.Content.ToString());
            return;
            
        }

        private void Function_1_Argument_Click(object sender, RoutedEventArgs e)
        {
            ClearMemoryTextField();
            Button button = (Button)sender;
            string operation = button.Content.ToString();
            
            if (CurrentNumberTextField.Text != "")
            {
                CurrentNumberTextField.Text = MathematicalBackground.Perform1ArgumentFunction(CurrentNumberTextField.Text, operation).ToString();
                IsLastButton1ArgFunction = true;
            }
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumberTextField.Text = "";
            IsLastButton1ArgFunction = false;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            CurrentNumberTextField.Text = "";
            MemoryTextField.Text = "";
            IsLastButtonEquals = false;
            IsLastButton1ArgFunction = false;
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
                    CurrentNumberTextField.Text += DecimalSeparator;
                }
            }
        }
        //do obsługiwania klawiatury numerycznej
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Decimal)
            {
                Comma_Click(sender, (RoutedEventArgs)e);
                return;
            }
            else if (e.Key==Key.Add)
            {
                Write_Operation("+"); 
                return;
            }
            else if (e.Key == Key.Subtract)
            {
                Write_Operation("-"); 
                return;
            }
            else if (e.Key == Key.Multiply)
            {
                Write_Operation("*"); 
                return;
            }
            else if (e.Key == Key.Divide)
            {
                Write_Operation("/"); 
                return;
            }
            else if (e.Key == Key.Return)
            {
                Equals_Click(sender, (RoutedEventArgs)e); 
                return;
            }
           
            else if (e.Key>=Key.NumPad0&&e.Key<=Key.NumPad9)
            {
               Write_Number( (e.Key - Key.NumPad0).ToString());
                
            }
            else if(e.Key >= Key.D0 && e.Key <= Key.D9)
            {
               Write_Number((e.Key - Key.D0).ToString());
                
            }
            
        }
    }
}
