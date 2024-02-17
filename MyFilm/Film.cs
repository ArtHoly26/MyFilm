using Newtonsoft.Json;
using System.ComponentModel;

namespace MyFilm
{
    public class Film : INotifyPropertyChanged
    {
        private int _idFilm;
        private string? _title;
        private DateTime? _releaseDate;
        private string? _owerview;
        private string? _posterPath;
        private double _voteAverage;
        private double _voteCount;

        
        [JsonProperty("id")]
        public int IdFilm
        {
            get { return _idFilm; }
            set
            {
                if (_idFilm != value)
                {
                    _idFilm = value;
                    OnPropertyChanged(nameof(IdFilm));
                }
            }
        }

        [JsonProperty("title")]
        public string? Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                if (_releaseDate != value)
                {
                    _releaseDate = value;
                    OnPropertyChanged(nameof(ReleaseDate));
                }
            }
        }

        [JsonProperty("overview")]
        public string Overview
        {
            get { return _owerview; }
            set
            {
                if (_owerview != value)
                {
                    _owerview = value;
                    OnPropertyChanged(nameof(Overview));
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

        [JsonProperty("vote_average")]
        public double VoteAverage
        {
            get { return _voteAverage; }
            set
            {
                if (_voteAverage != value)
                {
                    _voteAverage = value;
                    OnPropertyChanged(nameof(VoteAverage));
                }
            }
        }

        [JsonProperty("vote_count")]
        public double VoteCount
        {
            get { return _voteCount; }
            set
            {
                if (_voteCount != value)
                {
                    _voteCount = value;
                    OnPropertyChanged(nameof(VoteCount));
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
