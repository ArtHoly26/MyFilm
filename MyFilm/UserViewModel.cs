using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyFilm.SearchResult;

namespace MyFilm
{
    class UserViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Film _film;
        private List<string> countrylist;
        private SearchResult _results;
        private List<Film> _filmList;

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
        public List<string> countryList
        {
            get { return countrylist; }
            set
            {
                if (countrylist != value)
                {
                    countrylist = value;
                    OnPropertyChanged(nameof(countrylist));
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
