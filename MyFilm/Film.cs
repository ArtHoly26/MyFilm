using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyFilm
{
    public class Film : INotifyPropertyChanged
    {
        private int _id;
        private string? _name;
        private DateTime? _year;
        private List<int> _genre;
        private string _description;
        private string _duration;
        private string _rating;
        private string _posterPath;

        [JsonProperty("id")]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        [JsonProperty("title")]
        public string Title
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(nameof(ReleaseDate));
                }
            }
        }

        [JsonProperty("genre_ids")]
        public List<int> Genre
        {
            get { return _genre; }
            set
            {
                if (_genre != value)
                {
                    _genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        [JsonProperty("poster_path")]
        public string PosterPath
        {
            get { return _posterPath; }
            set
            {
                if (_posterPath != value)
                {
                    _posterPath = value;
                    OnPropertyChanged(nameof(PosterPath));
                }
            }
        }

        [JsonProperty("overview")]
        public string Overview
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Overview));
                }
            }
        }

        [JsonProperty("runtime")]
        public string Runtime
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Runtime));
                }
            }
        }

        [JsonProperty("vote_average")]
        public string VoteAverage
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(VoteAverage));
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
