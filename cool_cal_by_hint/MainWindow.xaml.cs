﻿using cool_cal_by_hint;
using cool_cal_by_hint.masterData;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cool_cal_by_hint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // System.Windows.Application.Current.Shutdown();
            SpecProducts.initData();

        }
        private void DisplayEvaporator(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("DisplayEvaporator");
        }
        private void DisplayCondenser(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            //MessageBox.Show("DisplayCondenser");
            Condenser Condenser = new Condenser();
            Condenser.Show();
            // condenser_btn.Background = Brushes.Turquoise;
        }


    }
}