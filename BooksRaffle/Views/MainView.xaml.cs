using System.Windows;
using BooksRaffle.ViewModels;

namespace BooksRaffle.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        public MainView(MainViewModel mainViewModel)
            : this()
        {
            DataContext = mainViewModel;
        }
    }
}