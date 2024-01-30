using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFilm
{
    class UserViewModel : INotifyPropertyChanged
    {
        private User _user;
        private List<string> countrylist;
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
