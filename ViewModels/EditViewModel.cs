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
    public class EditViewModel : INotifyPropertyChanged
    {
        private readonly Request? _original;

        public string RequestNumber { get; set; } = "";
        public string Title { get; set; } = "";
        public string? Type { get; set; }
        public string? ProblemDescription { get; set; }
        public DateOnly? CreatedDate { get; set; }
        public int Status { get; set; } = 0;

        public EditViewModel(Request? request = null)
        {
            _original = request;
            if (request != null)
            {
                RequestNumber = request.RequestNumber;
                Title = request.Title;
                Type = request.Type;
                ProblemDescription = request.ProblemDescription;
                CreatedDate = request.CreatedDate;
                Status = request.Status;
            }
            else
            {
                CreatedDate = DateOnly.FromDateTime(DateTime.Now);
            }
        }

        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(RequestNumber) || string.IsNullOrWhiteSpace(Title))
                return false;

            using var db = new MakarovContext();

            var exists = db.Requests
                .Any(r => r.RequestNumber == RequestNumber && (_original == null || r.Id != _original.Id));
            if (exists)
                return false;

            if (_original == null)
            {
                var newRequest = new Request
                {
                    RequestNumber = RequestNumber,
                    Title = Title,
                    Type = Type,
                    ProblemDescription = ProblemDescription,
                    CreatedDate = CreatedDate,
                    Status = Status
                };
                db.Requests.Add(newRequest);
            }
            else
            {
                var existing = db.Requests.Find(_original.Id);
                if (existing == null) return false;

                existing.RequestNumber = RequestNumber;
                existing.Title = Title;
                existing.Type = Type;
                existing.ProblemDescription = ProblemDescription;
                existing.CreatedDate = CreatedDate;
                existing.Status = Status;
            }

            db.SaveChanges();
            return true;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
