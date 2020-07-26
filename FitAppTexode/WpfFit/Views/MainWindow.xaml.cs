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

            //IConfiguration configuration = new ConfigurationBuilder()
            //  .AddJsonFile("appsettings.json", true, true)
            //  .Build();
            //string directory = configuration.GetSection("Directory").Value;
            //IErrorHandler errorHandler = new DefaultErrorHandler();
            //IFileReader fileReader = new JsonFileReader(errorHandler, directory);
            //IDialogService dialogService = new DefaultDialogService();
            //IFileService fileService = new JsonFileService(fileReader);

            DataContext = new MainViewModel(fileService, dialogService);
        }
    }
}
