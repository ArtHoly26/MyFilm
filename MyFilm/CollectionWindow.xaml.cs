using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace MyFilm
{
    public partial class CollectionWindow : Window
    {
        private UserViewModel userViewModel;
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public CollectionWindow(UserViewModel userViewModel)
        {
            InitializeComponent();
            this.userViewModel = userViewModel;
            DataContext = this.userViewModel;
        }
        private void Button_Click_ShowCollection(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT Title FROM AddFilm WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userViewModel.User.Id);

                    ObservableCollection<Film> myFilm = new ObservableCollection<Film>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Film movie = new Film
                            {
                                Title = reader["Title"].ToString(), 
                            };

                            myFilm.Add(movie);
                        }
                    }

                    userViewModel.MyFilm = myFilm;
                }
            }
        }
        private void ListBoxFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxFilm.SelectedItem != null)
            {
                Film selectedFilm = (Film)listBoxFilm.SelectedItem;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT * FROM AddFilm WHERE UserID = @UserID AND Title = @Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Title", selectedFilm.Title);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime releaseDate = (DateTime)reader["ReleaseDate"];
                                string overview = reader["Overview"].ToString();
                                string posterPath = reader["PosterPath"].ToString();
                                double voteAverage = (double)reader["VoteAverage"];
                                int voteCount = (int)reader["VoteCount"];

                                string formattedDate = ((DateTime)releaseDate).ToString("yyyy");
                                textReleaseBlock.Text += $"Год: {formattedDate}\n";
                                
                                textInfoBlock.Text += $"Описание: {overview}\n";

                                BitmapImage bitmapImage = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w500/{posterPath}"));
                                posterImg.Source = bitmapImage;

                                textRatingBlock.Text += $"Среднняя оценка: {voteAverage} Всего голосов: {voteCount}";
                            }
                        }
                    }
                }
            }
        }
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow(userViewModel);
            mainMenuWindow.Show();
            this.Close();
        }
        private void Button_Click_CleanAll(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM AddFilm WHERE UserID = @UserID;";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userViewModel.User.Id);

                    try
                    {
                        command.ExecuteNonQuery();
                        userViewModel.MyFilm.Clear();
                        string errorMessage = "Данные удалены!";
                        textError.Text = errorMessage;
                        textError.Foreground = Brushes.Green;
                        textInfoBlock.Text = "";
                        textRatingBlock.Text = "";
                        textReleaseBlock.Text = "";
                        posterImg.Source = null;
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = $"Ошибка удаления данных {ex.Message}";
                        textError.Text = errorMessage;
                        textError.Foreground = Brushes.Red;
                    }
                }
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (listBoxFilm.SelectedItem != null)
            {
                Film selectedFilm = (Film)listBoxFilm.SelectedItem;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM AddFilm WHERE UserID = @UserID AND Title=@Title";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userViewModel.User.Id);
                        command.Parameters.AddWithValue("@Title", selectedFilm.Title);

                        try
                        {
                            command.ExecuteNonQuery();
                            userViewModel.MyFilm.Remove(selectedFilm);
                            string errorMessage =  "Фильм удален из избранного!";
                            textError.Text = errorMessage;
                            textError.Foreground = Brushes.Green;
                            textInfoBlock.Text = "";
                            textRatingBlock.Text = "";
                            textReleaseBlock.Text = "";
                            posterImg.Source = null;

                        }
                        catch (Exception ex)
                        {
                            string errorMessage = $"Ошибка удаления данных {ex.Message}";
                            textError.Text = errorMessage;
                            textError.Foreground = Brushes.Red;
                        }
                    }
                }
            }
        }
    }
}