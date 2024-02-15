using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using static System.Net.Mime.MediaTypeNames;

namespace MyFilm
{
    /// <summary>
    /// Interaction logic for PrivateOfficeWindow.xaml
    /// </summary>
    public partial class PrivateOfficeWindow : Window
    {
        private UserViewModel userViewModel;
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public PrivateOfficeWindow(UserViewModel userViewModel)
        {
            InitializeComponent();
            this.userViewModel = userViewModel;
            DataContext = this.userViewModel;
            userViewModel.CountryList = new List<Country>()
            {
                 new Country { Name = "Россия"},
                 new Country { Name = "США"},
                 new Country { Name = "Италия"},
                 new Country { Name = "Уругвай"}
            };

            DataContext=userViewModel;
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainWindow = new MainMenuWindow(userViewModel);
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_ChangeData(object sender, RoutedEventArgs e)
        {
            string newFirstName = firstNameText.Text;
            string newLastName = lastNameText.Text;
            string newPartonymic = patronymicText.Text;
            string newEmail = emailText.Text;
            string newLogin = loginText.Text;
            DateTime newDateOfBirth = datePicker.SelectedDate ?? DateTime.MinValue;
            string newCountry = countryComboBox.Text;

            if (newDateOfBirth != DateTime.MinValue)
            {
                newDateOfBirth = datePicker.SelectedDate ?? DateTime.MinValue;
            }
            else
            {
                DateTime.TryParse(textDateOfBirth.Text, out DateTime parsedDate);
                newDateOfBirth = parsedDate;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateSql = "UPDATE UserData SET FirstName = @FirstName, LastName = @LastName, Patronymic=@Patronymic, Email=@Email, Login=@Login, DateOfBirth=@DateOfBirth, Country=@Country WHERE ID = @Id";

                    using (SqlCommand command = new SqlCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", newFirstName);
                        command.Parameters.AddWithValue("@LastName", newLastName);
                        command.Parameters.AddWithValue("@Patronymic", newPartonymic);
                        command.Parameters.AddWithValue("@Email", newEmail);
                        command.Parameters.AddWithValue("@Login", newLogin);
                        command.Parameters.AddWithValue("@DateOfBirth", newDateOfBirth);
                        command.Parameters.AddWithValue("@Country", newCountry);
                        command.Parameters.AddWithValue("@Id", userViewModel.User.Id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            string errorMessage = "Данные успешно изменены";
                            errorText.Text = errorMessage;
                            errorText.Foreground = Brushes.Green;
                        }
                        else
                        {
                            string errorMessage = "Ошибка изменение данных";
                            errorText.Text = errorMessage;
                            errorText.Foreground = Brushes.Green;
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
