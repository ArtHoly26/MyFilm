using System.Windows;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.DirectoryServices;
using System.Windows.Input;



namespace MyFilm
{
    public partial class FilmWindow : Window
    {
        public FilmWindow()
        {
            InitializeComponent();
            searchFilmBox.KeyDown += searchFilmBox_KeyDown;
        }

        private async Task<Film> SearchTVShowAsync(string query)
        {
            string apiKey = "71bcbe2bc2ebb0397066f331b1910ef3";
            string searchBaseUrl = "https://api.themoviedb.org/3/search/movie";

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{searchBaseUrl}?api_key={apiKey}&query={query}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();

                    try
                    {
                        // Попытаемся десериализовать как список
                        var searchResult = JsonConvert.DeserializeObject<List<Film>>(jsonData);
                        return searchResult.FirstOrDefault();
                    }
                    catch (JsonSerializationException)
                    {
                        // Если десериализация как список не удалась, попробуем как единичный объект
                        var singleFilm = JsonConvert.DeserializeObject<Film>(jsonData);
                        return singleFilm;
                    }
                }

                throw new Exception($"Ошибка при выполнении запроса: {response.StatusCode}");
            }
        }
        private async void Button_Click_SearchFilm (object sender, RoutedEventArgs e)
        {
            await PerformSearch();
        }
        private async void searchFilmBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await PerformSearch();
            }
        }
        private async Task PerformSearch()
        {
            string searchQuery = searchFilmBox.Text;

            try
            {
                Film searchResult = await SearchTVShowAsync(searchQuery);

                if (searchResult != null)
                {
                    textBlockName.Text = $"Name: {searchResult.Name}";
                    textBlockYear.Text = $"Year: {searchResult.Year}";
                    // и так далее для других свойств
                    
                }
                else
                {
                    MessageBox.Show("Нет результатов поиска.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

    }
}

