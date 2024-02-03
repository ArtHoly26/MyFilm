using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFilm
{
    public class User: INotifyPropertyChanged
    {
        private int _id;
        private string? _firstname;
        private string? _lastname;
        private string? _patronymic;
        private string? _email;
        private string? _login;
        private string? _password;
        private DateTime _dateofbirth;
        private int _age;
        private string? _country;
       
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
        public string FirstName
        {
            get { return _firstname; }
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        public string LastName
        {
            get { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                if (_patronymic != value)
                {
                    _patronymic = value;
                    OnPropertyChanged(nameof(Patronymic));
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateofbirth; }
            set
            {
                if (_dateofbirth != value)
                {
                    _dateofbirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                }
            }
        }
        private int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }
        public string Country
        {
            get { return _country; }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
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
