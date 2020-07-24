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
using WpfFit.Models;

namespace WpfFit.ViewModels
{
    public class MainViewModel : BaseVM
    {
        public const string directory = @"D:\Projects\Test tasks\ТЗ Jun C#\texode\TestData";
        public ObservableCollection<User> Users { get; set; }

        private User selectedUser;

        public MainViewModel()
        {
            Users = new ObservableCollection<User>();
            GetUsersStatistic();
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

        private async void GetUsersStatistic()
        {
            var allStatistic = await ProcessDirectory(directory);

            var usersData = new List<User>();

            foreach (var day in allStatistic)
            {
                foreach (UserInformationForADay data in day.Value)
                {
                    var existedUsers = Users.Where(u => u.UserName == data.User).ToList();
                    User user = null;
                    if (existedUsers.Count == 0)
                    {
                        user = new User
                        {
                            UserName = data.User,
                            UserData = new Dictionary<int, UserInformationForADay>()
                        };
                        user.UserData.Add(day.Key, data);
                        Users.Add(user);
                    }
                    else
                    {
                        user = existedUsers[0];
                        user.UserData.Add(day.Key, data);
                    }
                }
            }

            foreach (var user in Users)
            {
                var steps = user.UserData.Select(u => u.Value.Steps);
                user.AverageStepsNumber = (int)steps.Average();
                user.TheBestResult = steps.Max();
                user.TheWorstResult = steps.Min();
            }
        }

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        private static async Task<Dictionary<int, List<UserInformationForADay>>> ProcessDirectory(string targetDirectory)
        {
            var allStatistic = new Dictionary<int, List<UserInformationForADay>>();

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                // Only files with format name 'day1', 'day15', ...  are accepted
                if (!int.TryParse(Regex.Match(fileName, "\\d+").Value, out int dayNumber))
                {
                    continue;
                }

                try
                {
                    var dayStatistic = await ProcessFile(fileName);
                    allStatistic.Add(dayNumber, dayStatistic);
                }
                catch (JsonException jsonex)
                {
                    // TODO: bad file format notification
                }
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                await ProcessDirectory(subdirectory);

            return allStatistic;
        }

        /// <summary>
        /// Get data about users for a specific day.
        /// </summary>
        /// <param name="path">JSON file path.</param>
        private static async Task<List<UserInformationForADay>> ProcessFile(string path)
        {
            using FileStream fs = File.OpenRead(path);
            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new JsonStringEnumConverter { });
            return await JsonSerializer.DeserializeAsync<List<UserInformationForADay>>(fs, serializerOptions);
        }
    }
}