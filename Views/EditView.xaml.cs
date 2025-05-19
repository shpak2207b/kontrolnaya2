using System.Windows;
using kontrolnaya.Models;
using kontrolnaya.ViewModels;

namespace kontrolnaya.Views
{
    public partial class EditView : Window
    {
        public EditViewModel ViewModel { get; }

        public EditView(Request? request = null)
        {
            InitializeComponent();
            ViewModel = new EditViewModel(request);
            DataContext = ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Save())
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении. Проверьте данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
