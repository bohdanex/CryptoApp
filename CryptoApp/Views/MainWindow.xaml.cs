using CryptoApp.ViewModels;
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
using CryptoApp.Models;

namespace CryptoApp
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

        //Used to unfocus textbox
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Grid).Focus();
        }

        //Find in currencies
        async private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
