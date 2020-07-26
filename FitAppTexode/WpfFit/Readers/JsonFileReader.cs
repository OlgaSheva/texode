using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfFit.Models;

namespace WpfFit.Readers
{
    /// <summary>
    /// JSON file reader.
    /// </summary>
    public class JsonFileReader : IFileReader
    {
        private readonly string _directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFileReader"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentNullException">Configuration is null.</exception>
        public JsonFileReader(string directory)
        {
            _directory = directory ?? throw new ArgumentNullException(nameof(directory));
        }

        /// <summary>
        /// Get data about users for the entire period.
        /// </summary>
        /// <param name="path">JSON file path.</param>
        /// <returns>
        ///     A <see cref="Task{IDictionary{int,IList{UserInformationForADay}}}">.
        ///     Returns a dictionary where the key is the number of the day and the value is information about the users on that day.
        /// </returns>
        public async Task<IDictionary<int, IList<UserInformationForADay>>> ReadDirectory(string targetDirectory = null)
        {
            if (targetDirectory == null)
            {
                targetDirectory = _directory;
            }

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            IDictionary<int, IList<UserInformationForADay>> allStatistic = await ReadFiles(fileEntries);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                await ReadDirectory(subdirectory);

            return allStatistic;
        }

        public async Task<IDictionary<int, IList<UserInformationForADay>>> ReadFiles(string[] files)
        {
            var allStatistic = new Dictionary<int, IList<UserInformationForADay>>();
            foreach (string fileName in files)
            {
                // Only files with format name 'day1', 'day15', ...  are accepted
                if (!int.TryParse(Regex.Match(fileName, "\\d+").Value, out int dayNumber))
                {
                    continue;
                }

                try
                {
                    var dayStatistic = await ReadFile(fileName);
                    allStatistic.Add(dayNumber, dayStatistic);
                }
                catch (JsonException jsonex)
                {
                    // TODO: bad file format notification
                }
            }

            return allStatistic;
        }

        /// <summary>
        /// Get data about users for a specific day.
        /// </summary>
        /// <param name="path">JSON file path.</param>
        /// <returns>
        ///     A <see cref="Task{IList{UserInformationForADay}}">.
        ///     Returns a list with the information about the users on the day.
        /// </returns>
        public async Task<IList<UserInformationForADay>> ReadFile(string path)
        {
            using FileStream fs = File.OpenRead(path);
            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.Converters.Add(new JsonStringEnumConverter { });
            return await JsonSerializer.DeserializeAsync<IList<UserInformationForADay>>(fs, serializerOptions);
        }
    }
}
