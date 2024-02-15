using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyFilm
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Film _film;
        private List<Country> _countrylist;
        private SearchResult _results;
        private DetailFilm _detailFilm;
        private List<Film> _filmList;
        private ObservableCollection<Film> _myFilm;

        public User User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }
        public Film Film
        {
            get { return _film; }
            set
            {
                if (_film != value)
                {
                    _film = value;
                    OnPropertyChanged(nameof(Film));
                }
            }
        }
        public List<Country> CountryList
        {
            get { return _countrylist; }
            set
            {
                if (_countrylist != value)
                {
                    _countrylist = value;
                    OnPropertyChanged(nameof(CountryList));
                }
            }
        }

        [JsonProperty("results")]
        public SearchResult Result
        {
            get { return _results; }
            set
            {
                if (_results != value)
                {
                    _results = value;
                    OnPropertyChanged(nameof(Result));
                }
            }
        }
        public DetailFilm DetailFilm
        {
            get { return _detailFilm; }
            set
            {
                if (_detailFilm != value)
                {
                    _detailFilm = value;
                    OnPropertyChanged(nameof(DetailFilm));
                }
            }
        }
        public List<Film> FilmList
        {
            get { return _filmList; }
            set
            {
                if (_filmList != value)
                {
                    _filmList = value;
                    OnPropertyChanged(nameof(FilmList));
                }
            }
        }

        public ObservableCollection<Film> MyFilm
        {
            get { return _myFilm; }
            set
            {
                if (_myFilm != value)
                {
                    _myFilm = value;
                    OnPropertyChanged(nameof(MyFilm));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
