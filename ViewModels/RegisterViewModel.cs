using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using kontrolnaya.Models;

namespace kontrolnaya.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private string _login = "";
        private string _fullName = "";
        private string? _phoneNumber;

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }

        public string FullName
        {
            get => _fullName;
            set { _fullName = value; OnPropertyChanged(); }
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public bool TryRegister(string password)
        {
            using var db = new MakarovContext();

            if (db.Users.Any(u => u.Login == Login))
                return false;

            var user = new User
            {
                Login = Login,
                Password = password,
                FullName = FullName,
                PhoneNumber = PhoneNumber,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            db.Users.Add(user);
            db.SaveChanges();

            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
