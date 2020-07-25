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
using WpfFit.Models;
using WpfFit.Readers;
using WpfFit.Services;

namespace WpfFit.ViewModels
{
    public class MainViewModel : BaseVM
    {
        private User selectedUser;

        public NotifyTaskCompletion<IList<User>> Users { get; private set; }

        public MainViewModel(IFileService fileService)
        {
            Users = new NotifyTaskCompletion<IList<User>>(fileService.GetUsersStatistic());
        }

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }
    }
}