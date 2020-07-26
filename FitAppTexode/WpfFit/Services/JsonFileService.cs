using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WpfFit.Models;
using WpfFit.Readers;

namespace WpfFit.Services
{
    /// <summary>
    /// JSON file service.
    /// </summary>
    public class JsonFileService : IFileService
    {
        private readonly IFileReader _fileReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFileService"/> class.
        /// </summary>
        /// <param name="fileReader">File reader.</param>
        /// <exception cref="ArgumentNullException">File reader is null.</exception>
        public JsonFileService(IFileReader fileReader)
        {
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        /// <summary>
        /// Get all users statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{User}}".</returns>
        public async Task<IList<User>> GetUsersStatistic()
        {
            var allStatistic = await _fileReader.ReadDirectory();
            return GetUsers(allStatistic);
        }

        /// <summary>
        /// Get all users statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{User}}".</returns>
        public async Task<IList<User>> GetUsersStatistic(string[] files)
        {
            var allStatistic = await _fileReader.ReadFiles(files);
            return GetUsers(allStatistic);
        }

        private static IList<User> GetUsers(IDictionary<int, IList<UserInformationForADay>> allStatistic)
        {
            var users = new List<User>();

            var usersData = new List<User>();

            foreach (var day in allStatistic)
            {
                foreach (UserInformationForADay data in day.Value)
                {
                    var existedUsers = users.Where(u => u.UserName == data.User).ToList();
                    User user = null;
                    if (existedUsers.Count == 0)
                    {
                        user = new User
                        {
                            UserName = data.User,
                            UserData = new Dictionary<int, UserInformationForADay>()
                        };
                        user.UserData.Add(day.Key, data);
                        users.Add(user);
                    }
                    else
                    {
                        user = existedUsers[0];
                        user.UserData.Add(day.Key, data);
                    }
                }
            }

            GetTheAverageMaximumAndMinimumResults(users);

            return users;
        }

        private static void GetTheAverageMaximumAndMinimumResults(List<User> users)
        {
            foreach (var user in users)
            {
                var steps = user.UserData.Select(u => u.Value.Steps);
                user.AverageStepsNumber = (int)steps.Average();
                user.TheBestResult = steps.Max();
                user.TheWorstResult = steps.Min();
            }
        }
    }
}
