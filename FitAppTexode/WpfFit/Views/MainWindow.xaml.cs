using System.Windows;
using WpfFit.Services;
using WpfFit.ViewModels;

namespace WpfFit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IFileService fileService, IDialogService dialogService)
        {
            InitializeComponent();

            DataContext = new MainViewModel(fileService, dialogService);
        }
    }
}
