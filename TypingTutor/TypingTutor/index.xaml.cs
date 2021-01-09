using System;
using System.Collections.Generic;
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

namespace TypingTutor
{
    /// <summary>
    /// Interaction logic for index.xaml
    /// </summary>
    public partial class index : Window
    {
        public index()
        {
            InitializeComponent();
        }

        private void click(object sender, MouseButtonEventArgs e)
        {
            MainWindow random = new MainWindow();
            random.Show();
            this.Hide();
        }

        private void click_1(object sender, MouseButtonEventArgs e)
        {
            essay es = new essay();
            es.Show();
            this.Hide();
        }
    }
}
