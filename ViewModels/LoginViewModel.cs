using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using kontrolnaya.Models;
using kontrolnaya.Views;

namespace kontrolnaya.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login = "";
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public User? CurrentUser { get; private set; }

        public bool TryLogin(string password)
        {
            using var db = new MakarovContext();
            var user = db.Users.FirstOrDefault(u => u.Login == Login && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
