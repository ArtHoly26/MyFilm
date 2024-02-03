using System.Windows;
using System.Diagnostics;
using System.Windows.Media;


namespace MyFilm
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.Show();
            this.Close();
        }

        private void Button_Click_URL(object sender, RoutedEventArgs e)
        {
            string url = "https://vk.com/o.zelenskii";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory 
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                errorText.Text = "Ошибка при переходе по ссылке!";
                errorText.Foreground = Brushes.Red;
            }
        }
    }
}