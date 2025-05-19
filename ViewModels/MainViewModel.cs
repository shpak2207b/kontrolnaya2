using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using kontrolnaya.Models;
using kontrolnaya.Views;

using System.Windows.Input;

namespace kontrolnaya.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly User _currentUser;

        public ObservableCollection<Request> Requests { get; set; }

        private Request? _selectedRequest;
        public Request? SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                _selectedRequest = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }


        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AssignCommand { get; }
        public ICommand UnassignCommand { get; }

        public MainViewModel(User user)
        {
            _currentUser = user;
            using var db = new MakarovContext();
            Requests = new ObservableCollection<Request>(db.Requests.ToList());

            EditCommand = new RelayCommand(_ => EditRequest(), _ => SelectedRequest != null);
            DeleteCommand = new RelayCommand(_ => DeleteRequest(), _ => SelectedRequest != null);
            AssignCommand = new RelayCommand(_ => AssignRequest(), _ => SelectedRequest != null && SelectedRequest.MasterId == null);
            UnassignCommand = new RelayCommand(_ => UnassignRequest(), _ => SelectedRequest != null && SelectedRequest.MasterId == _currentUser.Id);

        }

        private void AddRequest()
        {
            var window = new EditView();
            if (window.ShowDialog() == true)
                Refresh();
        }

        private void EditRequest()
        {
            if (SelectedRequest == null) return;
            var window = new EditView(SelectedRequest);
            if (window.ShowDialog() == true)
                Refresh();
        }

        private void DeleteRequest()
        {
            if (SelectedRequest == null) return;
            using var db = new MakarovContext();
            var req = db.Requests.Find(SelectedRequest.Id);
            if (req != null)
            {
                db.Requests.Remove(req);
                db.SaveChanges();
                Refresh();
            }
        }

        private void AssignRequest()
        {
            using var db = new MakarovContext();
            var req = db.Requests.Find(SelectedRequest!.Id);
            if (req != null)
            {
                req.MasterId = _currentUser.Id;
                req.Status = 1; // В работе
                db.SaveChanges();
                Refresh();
            }
        }

        private void UnassignRequest()
        {
            using var db = new MakarovContext();
            var req = db.Requests.Find(SelectedRequest!.Id);
            if (req != null)
            {
                req.MasterId = null;
                req.Status = 0; // В обработке
                db.SaveChanges();
                Refresh();
            }
        }

        private void Refresh()
        {
            using var db = new MakarovContext();
            var fresh = db.Requests.ToList();

            Requests.Clear();
            foreach (var r in fresh)
                Requests.Add(r);

            SelectedRequest = null;
            OnPropertyChanged(nameof(SelectedRequest));
            OnPropertyChanged(nameof(Requests));
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
