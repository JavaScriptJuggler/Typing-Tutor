using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TypingTutor
{
    /// <summary>
    /// Interaction logic for essay.xaml
    /// </summary>
    public partial class essay : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        string[] files = Directory.GetFiles("Texts");
        string[] word = new string[0];
        public essay()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        int i = 0;
        bool tip_changer = true;
        string location;
        private void timer_tick(object sender, EventArgs e)
        {
            if (i != 0)
            {
                if (bar.Value < i)
                {
                    bar.Value++;
                    label2.Content = "Time Elapsed: " + bar.Value;
                }
                else
                    timer.Stop();
            }
        }

        private void filelocation()
        {
            location = "Texts\\" + comboBox_Copy.SelectedItem;
            //MessageBox.Show(location);
        }
        private void load(object sender, RoutedEventArgs e)
        {
            comboBox.Items.Add("--Select--");
            comboBox.Items.Add("10 Minutes");
            comboBox.Items.Add("20 Minutes");
            comboBox.Items.Add("30 Minutes");
            comboBox.Items.Add("45 Minutes");
            comboBox.Items.Add("1 Hour");
            comboBox.Items.Add("Disabled");
            bar.Value = 0;
            comboBox.SelectedIndex = 0;
            comboBox_Copy.SelectedIndex = 0;
            textBox.IsEnabled = false;
            textBox.Foreground = new SolidColorBrush(Color.FromRgb(188, 188, 188));
            label2.Content = "Select proper option and press Start Button";

            foreach (string file in files)
            {
                comboBox_Copy.Items.Add(System.IO.Path.GetFileName(file));
            }
        }
        private void Timer_Calculation(int value)
        {
            bar.Maximum = (value * 60);
            i = (value * 60);
        }

        private void selection_change(object sender, SelectionChangedEventArgs e)
        {
            //if (comboBox.SelectedIndex != 6 && comboBox.SelectedIndex!=0)
            //{
            //    calculation();
            //    bar.Value = 0;
            //    timer.Start();
            //}
        }
        string[] list; string[] text1; char[] seperator;
        int check_index = 0;
        private void selection_change1(object sender, SelectionChangedEventArgs e)
        {
            filelocation();
            using (TextReader read = new StreamReader(location))
            {
                textBlock.Text = read.ReadLine();
            }
            seperator = new char[] { ' ' };
            list = textBlock.Text.Split(seperator);
            text1 = textBox.Text.Split(seperator);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.SelectedIndex != 6 && comboBox.SelectedIndex != 0)
            {
                calculation();
                bar.Value = 0;
                timer.Start();
            }
            else
                label2.Content = "Timer Disabled";
            comboBox.IsEnabled = false;
            comboBox_Copy.IsEnabled = false;
            button.Content = "Finish/Stop";
            button.Background = new SolidColorBrush(Colors.Red);
            button.Foreground = new SolidColorBrush(Colors.White);
            textBox.IsEnabled = true;
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            textBox.Clear();
            textBox.Focus();
            tip_changer = false;

            if (tip_changer == false)
            {
                button.ToolTip = "Finish";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdb = new OpenFileDialog();
            fdb.Filter = "Text Files|*.txt";
            Nullable<bool> result = fdb.ShowDialog();
            if (result == true)
            {
                string path = fdb.FileName;
                File.Copy(path, "Texts\\" + fdb.SafeFileName);
            }
        }
        private void calculation()
        {
            if (comboBox.SelectedIndex == 1)
                Timer_Calculation(10);
            else if (comboBox.SelectedIndex == 2)
                Timer_Calculation(20);
            else if (comboBox.SelectedIndex == 3)
                Timer_Calculation(30);
            else if (comboBox.SelectedIndex == 4)
                Timer_Calculation(45);
            else if (comboBox.SelectedIndex == 5)
                Timer_Calculation(60);
            else if (comboBox.SelectedIndex == 6)
                Timer_Calculation(0);
        }

        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            { e.Handled = true; }
            else
            {
                if (e.Key == Key.Space)
                {
                    string lastWord = textBox.Text.Split(' ')[textBox.Text.Split(' ').Count() - 1];
                    if (lastWord != list[check_index])
                    {
                        MessageBox.Show("Type Correctly", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    check_index++;
                }
            }
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBox.IsEnabled == false)
                MessageBox.Show("Press START TYPING", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
