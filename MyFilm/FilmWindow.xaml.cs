using System.Windows;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.DirectoryServices;




namespace MyFilm
{
    public partial class FilmWindow : Window
    {
        public FilmWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            try
            {
                string apiKey = "71bcbe2bc2ebb0397066f331b1910ef3";
                string searchQuery = searchTextBox.Text;

                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={searchQuery}&language=ru";
                   
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();

                        // Десериализация JSON-ответа в SearchResult
                        SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(jsonResult);

                        if (searchResult != null && searchResult.Results != null && searchResult.Results.Any())
                        {
                            Film firstMovie = searchResult.Results.First();

                            if (firstMovie != null)
                            {
                                if (firstMovie.Title != null)
                                {
                                    textTitleBlock.Text = $"Название: {firstMovie.Title}\n";
                                }

                                if (firstMovie.ReleaseDate != null)
                                {
                                    string formattedDate = ((DateTime)firstMovie.ReleaseDate).ToString("yyyy");
                                    textReleaseBlock.Text += $"Дата выхода: {formattedDate}\n";
                                }

                                if (firstMovie.Overview != null)
                                {
                                    textInfoBlock.Text += $"Описание: {firstMovie.Overview}\n";
                                }

                                if (searchResult != null && searchResult.Results != null && searchResult.Results.Any())
                                {

                                    if (!string.IsNullOrEmpty(firstMovie.PosterPath))
                                    {
                                        // Создаем объект BitmapImage из URL постера
                                        BitmapImage bitmapImage = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w500/{firstMovie.PosterPath}"));

                                        // Устанавливаем BitmapImage в качестве источника для элемента Image
                                        posterImg.Source = bitmapImage;
                                    }
                                    else
                                    {
                                        // Если PosterPath не является корректным URL, выполните соответствующие действия.
                                        Console.WriteLine("Некорректный URL постера.");
                                    }

                                    // Установка списка фильмов в ListBox
                                    listBoxFilm.ItemsSource = searchResult.Results;
                                }

                                if (firstMovie.VoteAverage != null && firstMovie.VoteCount != null)
                                {
                                    textRatingBlock.Text += $"Среднняя оценка: {firstMovie.VoteAverage} Всего голосов: {firstMovie.VoteCount}";
                                }

                                else
                                {
                                    textErrorBlock.Text = "Данные о фильме не содержат информации.";
                                }
                            }
                            else
                            {
                                textErrorBlock.Text = "Фильм не найден или отсутствует информация.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                textErrorBlock.Text = $"Произошла ошибка: {ex.Message}";
            }


        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _ = LoadDataAsync(); // Запускаем выполнение асинхронной работы
        }
        private void ListBoxFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textInfoBlock.Text = "";
            textRatingBlock.Text = "";
            textTitleBlock.Text = "";
            textReleaseBlock.Text = "";
            
            if (listBoxFilm.SelectedItem != null)
            {
                // Получите выбранный фильм из списка
                Film selectedFilm = (Film)listBoxFilm.SelectedItem;
                if (selectedFilm != null)
                {
                    if (selectedFilm.Title != null)
                    {
                        textTitleBlock.Text = $"Название: {selectedFilm.Title}\n";
                    }

                    if (selectedFilm.ReleaseDate != null)
                    {
                        string formattedDate = ((DateTime)selectedFilm.ReleaseDate).ToString("yyyy");
                        textReleaseBlock.Text += $"Год: {formattedDate}\n";
                    }

                    if (selectedFilm.Overview != null)
                    {
                        textInfoBlock.Text += $"Описание: {selectedFilm.Overview}\n";
                    }

                    if (!string.IsNullOrEmpty(selectedFilm.PosterPath))
                    {
                            // Создаем объект BitmapImage из URL постера
                            BitmapImage bitmapImage = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w500/{selectedFilm.PosterPath}"));

                            // Устанавливаем BitmapImage в качестве источника для элемента Image
                            posterImg.Source = bitmapImage;
                    }

                    if (selectedFilm.VoteCount != null && selectedFilm.VoteAverage != null)
                    {
                        textRatingBlock.Text += $"Среднняя оценка: {selectedFilm.VoteAverage} Всего голосов: {selectedFilm.VoteCount}";
                    }

                    else
                    {
                            // Если PosterPath не является корректным URL, выполните соответствующие действия.
                            Console.WriteLine("Некорректный URL постера.");
                    }
                }      
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

