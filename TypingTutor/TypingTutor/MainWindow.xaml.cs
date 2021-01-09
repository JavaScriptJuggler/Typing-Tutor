using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TypingTutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        char[] array = new char[0];
        int[] numarray = new int[0];
        public int i = 0, rg = 0, wr = 0;
        public MainWindow()
        {
            InitializeComponent();
           
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        protected void fillarray()
        {
            for(char ch='a'; ch<='z';ch++)
            {
                Array.Resize(ref array, array.Length + 1);
                array[i] = ch;
                i++;
            }
            for (char ch = 'A'; ch <= 'Z'; ch++)
            {
                Array.Resize(ref array, array.Length + 1);
                array[i] = ch;
                i++;
            }
            i = 0;
            for(int j=1;j<=7;j++)
            {
                Array.Resize(ref numarray, numarray.Length + 1);
                numarray[i] = j;
                i++;
            }
        }
        protected string rand()
        {
            int num = 0;string typing="";
                Random rand = new Random();
            int number = rand.Next(numarray.Length);
            while (num <= number)
            {
                int index = rand.Next(array.Length);
                typing =typing+ array[index];
                num++;
            }

            return typing;
        }
        protected void time()
        {
            progressbar.Maximum = 20;
            timer.Start();
        }
      
        protected void timer_tick(object sender,EventArgs e)
        {
            if (progressbar.Value<20)
            {
                progressbar.Value=progressbar.Value+1;
            }
            else
            {
                timer.Stop();
                MessageBox.Show("You are too Slow!!!", "Notification",MessageBoxButton.OK,MessageBoxImage.Warning);
                time();
                textBlock.Text = rand();
                textBox.Clear();
                progressbar.Value = 0;
            }
        }
       
        private void onload(object sender, RoutedEventArgs e)
        {
            time();
            fillarray();
            textBlock.Text = rand();
            textBox.Focus();
            radioButton_Copy.IsChecked = true;
        }

        private void close(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            time();
            textBlock.Text = rand();
            textBox.Clear();
        }

        private void key_press(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                if (radioButton_Copy.IsChecked == true)
                {
                    string a = textBlock.Text.ToUpper();
                    string b = textBlock.Text.ToLower();
                    if (textBox.Text == a || textBox.Text == b)
                    {
                        rg++;
                        label1_Copy1.Content = Convert.ToString(rg);
                    }
                    else
                    {
                        wr++;
                        label1_Copy2.Content = Convert.ToString(wr);
                    }
                }
                else
                {
                    if (textBlock.Text == textBox.Text)
                    {
                        rg++;
                        label1_Copy1.Content = Convert.ToString(rg);
                    }
                    else
                    {
                        wr++;
                        label1_Copy2.Content = Convert.ToString(wr);
                    }
                }
                time();
                textBlock.Text = rand();
                textBox.Clear();
            }
        }
    }
   
}
