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
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace MyFilm
{
    
    public partial class RegisterWindow : Window
    {
        public List<string> Country { get; set; }
        public RegisterWindow()
        {
            InitializeComponent();
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.countryList = new List<string>()
            {
                "Россия",
                "Белоруссия",
                "Канада"
            };

            DataContext = userViewModel;
        }

        private void Button_Exit_Menu(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_Register(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameText.Text;
            string lastName = lastNameText.Text;
            string patronymic = patronymicText.Text;
            string email = emailText.Text;
            DateTime selectedDate = datePicker.SelectedDate ?? DateTime.MinValue;
            string country = countryBox.Text;
            string login = loginText.Text;
            string password = passwordText.Text;
            string passwordRepeat = passwordRepeatText.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            try
            {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO UserData (FirstName,LastName,Patronymic,Email,Login,Password,DateOfBirth,Country)" +
                                           "VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Value1", firstName);
                            command.Parameters.AddWithValue("@Value2", lastName);
                            command.Parameters.AddWithValue("@Value3", patronymic);
                            command.Parameters.AddWithValue("@Value4", email);
                            command.Parameters.AddWithValue("@Value5", login);
                            command.Parameters.AddWithValue("@Value6", password);
                            command.Parameters.AddWithValue("@Value7", selectedDate);
                            command.Parameters.AddWithValue("@Value8", country);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0 && password==passwordRepeat)
                            {
                                errorText.Text="Регистрация прошла успешно!";
                                errorText.Foreground = Brushes.Green;
                            }

                            else
                            { 
                                errorText.Text = "Ошибка при вводе данных!";
                                errorText.Foreground = Brushes.Red;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                  MessageBox.Show($"Ошибка: {ex.Message}");
            }
            
        }
    }
    
}
