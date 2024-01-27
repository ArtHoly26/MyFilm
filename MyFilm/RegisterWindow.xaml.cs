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
using System.Windows.Shapes;

namespace MyFilm
{
    
    public partial class RegisterWindow : Window
    {
        public List<string> Country { get; set; }
        public RegisterWindow()
        {
            InitializeComponent();

            Country = new List<string>
            {
              "Россия",
              "Белоруссия",
              "Канада"
            };

            DataContext = this;
        }

        private void Button_Exit_Menu(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
