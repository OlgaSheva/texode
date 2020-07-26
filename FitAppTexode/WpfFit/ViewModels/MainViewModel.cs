using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using WpfFit.AsyncCommand;
using WpfFit.Helpers;
using WpfFit.Models;
using WpfFit.Services;
using WpfFit.Writers;

namespace WpfFit.ViewModels
{
    public class MainViewModel : BaseVM
    {
        #region private fields
        private readonly IFileService _fileService;
        private readonly IDialogService _dialogService;
        private User _selectedUser;
        private NotifyTaskCompletion<IList<User>> _users;
        private ChartValues<int> _selectedUserSteps;
        private ChartValues<int> _days;

        private AsyncCommand<NotifyTaskCompletion<IList<User>>> _openCommand;
        private AsyncCommand<User> _saveCommand;
        #endregion

        #region properties
        public NotifyTaskCompletion<IList<User>> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                if (_selectedUser != null)
                {
                    var selectedUserSteps = _selectedUser.UserData.Select(u => (u.Value.Steps));
                    SelectedUserSteps = new ChartValues<int>(selectedUserSteps);
                    var days = _selectedUser.UserData.Select(u => (u.Key));
                    Days = new ChartValues<int>(days);
                }
                else
                {
                    SelectedUserSteps = null;
                }
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ChartValues<int> SelectedUserSteps
        {
            get { return _selectedUserSteps; }
            set
            {
                _selectedUserSteps = value;

                // color the minimum and maximum points of the selected user steps graph
                if (value != null)
                {
                    ColorMinAndMaxPoint();
                }

                OnPropertyChanged(nameof(SelectedUserSteps));
            }
        }

        public ChartValues<int> Days
        {
            get { return _days; }
            set
            {
                _days = value;
                OnPropertyChanged(nameof(Days));
            }
        }
        #endregion

        public MainViewModel(IFileService fileService, IDialogService dialogService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            Users = new NotifyTaskCompletion<IList<User>>(_fileService.GetUsersStatistic());
        }

        #region open files command
        public AsyncCommand<NotifyTaskCompletion<IList<User>>> OpenCommand
        {
            get
            {
                return _openCommand ??= new AsyncCommand<NotifyTaskCompletion<IList<User>>>(async (obj) =>
                {
                    try
                    {
                        if (_dialogService.OpenFileDialog() == true)
                        {
                            Users = new NotifyTaskCompletion<IList<User>>(_fileService.GetUsersStatistic(_dialogService.FilePaths));
                            _dialogService.ShowMessage("Файлы открыты");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }
        #endregion

        #region save User command
        public AsyncCommand<User> SaveCommand
        {
            get
            {
                return _saveCommand ??= new AsyncCommand<User>(async (user) =>
                {
                    try
                    {
                        if (_dialogService.SaveFileDialog() == true)
                        {
                            var fileExtension = _dialogService.FileExtension;
                            using StreamWriter writer = new StreamWriter(File.Create(_dialogService.FilePath));
                            switch (fileExtension)
                            {
                                case ".xml":
                                    await new UserXmlWriter(writer).Write(user);
                                    break;
                                case ".json":
                                    await new UserJsonWriter(writer).Write(user);
                                    break;
                                case ".csv":
                                    await new UserCsvWriter(writer).Write(user);
                                    break;
                                default:
                                    throw new FileFormatException("Неподдерживаемый формат файла");
                            }

                            _dialogService.ShowMessage("Файл сохранен");
                        }
                    }
                    catch (Exception ex)
                    {
                        _dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }

        #endregion

        #region private helpers
        private void ColorMinAndMaxPoint()
        {
            var mapper = new CartesianMapper<int>()
                .X((value, index) => index)
                .Y(value => value)
                .Fill((value, index) =>
                value == SelectedUser?.TheBestResult ?
                    Brushes.Green : value == SelectedUser?.TheWorstResult ?
                        Brushes.Red : null);
            LiveCharts.Charting.For<int>(mapper, SeriesOrientation.Horizontal);
        }
        #endregion
    }
}