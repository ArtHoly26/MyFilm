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
    public partial class MainMenuWindow : Window
    {

        private UserViewModel userViewModel;
        public MainMenuWindow(UserViewModel userViewModel)
        {
            InitializeComponent();
            this.userViewModel = userViewModel;
            DataContext = this.userViewModel;
        }

        private void Button_Click_Film(object sender, RoutedEventArgs e)
        {
            FilmWindow filmWindow = new FilmWindow();
            filmWindow.Show();
            this.Close();
        }

        private void Button_Click_Office(object sender, RoutedEventArgs e)
        {
            PrivateOfficeWindow privateOfficeWindow = new PrivateOfficeWindow(userViewModel);
            privateOfficeWindow.Show();
            this.Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
