using LiveCharts;
using LiveCharts.Configurations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WpfFit.AsyncCommand;
using WpfFit.Helpers;
using WpfFit.Models;
using WpfFit.Readers;
using WpfFit.Services;
using WpfFit.Writers;

namespace WpfFit.ViewModels
{
    public class MainViewModel : BaseVM
    {
        private readonly IConfiguration _configuration;
        private User _selectedUser;
        private ChartValues<int> _selectedUserSteps;        

        public NotifyTaskCompletion<IList<User>> Users { get; set; }
        public ChartValues<int> Days { get; private set; }
        //public Brush DangerBrush { get; set; }
        //public CartesianMapper<ObservableValue> Mapper { get; set; }
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                var selectedUserSteps = _selectedUser.UserData.Select(u => (u.Value.Steps));
                SelectedUserSteps = new ChartValues<int>(selectedUserSteps);
                var days = _selectedUser.UserData.Select(u => (u.Key));
                Days = new ChartValues<int>(days);
                OnPropertyChanged("SelectedUser");
            }
        }
        public ChartValues<int> SelectedUserSteps
        {
            get { return _selectedUserSteps; }
            set
            {
                _selectedUserSteps = value;
                OnPropertyChanged("SelectedUserSteps");
            }
        }

        public MainViewModel(IConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            Users = new NotifyTaskCompletion<IList<User>>(fileService.GetUsersStatistic());

            //Mapper = Mappers.Xy<ObservableValue>()
            //    .X((item, index) => index)
            //    .Y(item => item.Value)
            //    .Fill(item => item.Value > 40000 ? DangerBrush : null)
            //    .Stroke(item => item.Value > 40000 ? DangerBrush : null);
            //DangerBrush = new SolidColorBrush(Color.FromRgb(238, 83, 80));
        }

        private AsyncCommand<User> _saveJsonCommand;
        public AsyncCommand<User> SaveJsonCommand
        {
            get
            {
                return _saveJsonCommand ??
                  (_saveJsonCommand = new AsyncCommand<User>(async (user) =>
                  {
                      await new UserJsonWriter(_configuration).Write(user);
                  }));
            }
        }

        private AsyncCommand<User> _saveXmlCommand;
        public AsyncCommand<User> SaveXmlCommand
        {
            get
            {
                return _saveXmlCommand ??
                  (_saveXmlCommand = new AsyncCommand<User>(async (user) =>
                  {
                      await new UserXmlWriter(_configuration).Write(user);
                  }));
            }
        }

        private AsyncCommand<User> _saveCsvCommand;
        public AsyncCommand<User> SaveCsvCommand
        {
            get
            {
                return _saveCsvCommand ??
                  (_saveCsvCommand = new AsyncCommand<User>(async (user) =>
                  {
                      await new UserCsvWriter(_configuration).Write(user);
                  }));
            }
        }
    }
}