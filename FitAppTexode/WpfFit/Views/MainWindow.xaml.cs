using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfFit.Readers;
using WpfFit.Services;
using WpfFit.ViewModels;

namespace WpfFit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            string directory = configuration.GetSection("Directory").Value;
            IFileReader fileReader = new JsonFileReader(directory);
            IFileService fileService = new JsonFileService(fileReader);
            IDialogService dialogService = new DefaultDialogService();

            DataContext = new MainViewModel(directory, fileService, dialogService);
        }
    }
}
