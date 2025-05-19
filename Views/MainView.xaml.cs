using System.Windows;
using kontrolnaya.Models;
using kontrolnaya.ViewModels;

namespace kontrolnaya.Views
{
    public partial class MainView : Window
    {
        public MainView(User currentUser)
        {
            InitializeComponent();
            DataContext = new MainViewModel(currentUser);
        }
    }
}
