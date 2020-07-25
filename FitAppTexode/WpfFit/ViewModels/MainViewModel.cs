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
using WpfFit.Helpers;
using WpfFit.Models;
using WpfFit.Readers;
using WpfFit.Services;

namespace WpfFit.ViewModels
{
    public class MainViewModel : BaseVM
    {
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

        public MainViewModel(IFileService fileService)
        {
            Users = new NotifyTaskCompletion<IList<User>>(fileService.GetUsersStatistic());

            //Mapper = Mappers.Xy<ObservableValue>()
            //    .X((item, index) => index)
            //    .Y(item => item.Value)
            //    .Fill(item => item.Value > 40000 ? DangerBrush : null)
            //    .Stroke(item => item.Value > 40000 ? DangerBrush : null);
            //DangerBrush = new SolidColorBrush(Color.FromRgb(238, 83, 80));
        }
    }
}