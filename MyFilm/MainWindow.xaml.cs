using System.Windows;
using System.Diagnostics;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;


namespace MyFilm
{
    
    public partial class MainWindow : Window
    {

        public string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
            UserViewModel userViewModel = new UserViewModel();
            string loginToCheck = textLoginToCheck.Text;
            string passwordToCheck = textPasswordToCheck.Password;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ID, Password From UserData WHERE Login=@Login";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", loginToCheck);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string hashedPasswordFromDatabase = reader.GetString(1);

                                using (SHA256 sha256 = SHA256.Create())
                                {
                                    byte[] hashedBytesToTextBox = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwordToCheck));
                                    byte[] hashedBytesTodDatabase = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashedPasswordFromDatabase));

                                    string hashedPasswordToCheck = BitConverter.ToString(hashedBytesToTextBox).Replace("-", "");
                                    hashedPasswordFromDatabase = BitConverter.ToString(hashedBytesTodDatabase).Replace("-", "");

                                    if (hashedPasswordToCheck == hashedPasswordFromDatabase)
                                    {
                                        using (SqlConnection connection1 = new SqlConnection(connectionString))
                                        {
                                            connection1.Open();
                                            string query1 = "SELECT ID, FirstName,LastName, Patronymic, Email, Login, Password, DateOfBirth, Country FROM UserData WHERE Login=@Login";

                                            using (SqlCommand command1 = new SqlCommand(query1, connection1))
                                            {
                                                command1.Parameters.AddWithValue("@Login", loginToCheck);

                                                using (SqlDataReader reader1 = command1.ExecuteReader())
                                                {
                                                    while (reader1.Read())
                                                    {
                                                        userViewModel = new UserViewModel
                                                        {
                                                            User = new User
                                                            {
                                                                Id = reader1.GetInt32(reader1.GetOrdinal("Id")),
                                                                FirstName = reader1["FirstName"].ToString(),
                                                                LastName = reader1["LastName"].ToString(),
                                                                Patronymic = reader1["Patronymic"].ToString(),
                                                                Email = reader1["Email"].ToString(),
                                                                Login = reader1["Login"].ToString(),
                                                                Password = reader1["Password"].ToString(),
                                                                DateOfBirth = Convert.ToDateTime(reader1["DateOfBirth"]),
                                                                Country = reader1["Country"].ToString()
                                                            }
                                                        };
                                                    };
                                                }
                                            }

                                        }

                                        MainMenuWindow mainMenuWindow = new MainMenuWindow(userViewModel);
                                        mainMenuWindow.Show();
                                        this.Close();
                                    }

                                    else
                                    {
                                        string errorMessage = "Неверный логин или пароль!";
                                        errorText.Text = errorMessage;
                                        errorText.Foreground = Brushes.Red;
                                    }
                                }
                            }

                            else
                            { 
                                string errorMessage = "Неверный логин или пароль!";
                                errorText.Text = errorMessage;
                                errorText.Foreground = Brushes.Red;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                string errorMessage = $"Произошла ошибка: {ex.Message}";
                errorText.Text = errorMessage;
                errorText.Foreground = Brushes.Red;
            }
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