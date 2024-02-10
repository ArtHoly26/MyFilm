using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFilm
{
    public class DetailFilm
    {
        private int _runTime;
        private int _budget;
        private int _revenue;
        private List<Genre> _genres;


        [JsonProperty("runtime")]
        public int Runtime
        {
            get { return _runTime; }
            set
            {
                if (_runTime != value)
                {
                    _runTime = value;
                    OnPropertyChanged(nameof(Runtime));
                }
            }
        }

        [JsonProperty("budget")]
        public int Budget
        {
            get { return _budget; }
            set
            {
                if (_budget != value)
                {
                    _budget = value;
                    OnPropertyChanged(nameof(Budget));
                }
            }
        }

        [JsonProperty("revenue")]
        public int Revenue
        {
            get { return _revenue; }
            set
            {
                if (_revenue != value)
                {
                    _revenue = value;
                    OnPropertyChanged(nameof(Revenue));
                }
            }
        }

        [JsonProperty("genres")]
        public List<Genre> Genres
        {
            get { return _genres; }
            set
            {
                if (_genres != value)
                {
                    _genres = value;
                    OnPropertyChanged(nameof(Genres));
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
