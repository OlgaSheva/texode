using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            ProcessDirectory(directory);
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

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        private static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        /// <summary>
        /// Get data about users for a specific day.
        /// </summary>
        /// <param name="path">JSON file path.</param>
        private static void ProcessFile(string path)
        {
            using (var fs = new StreamReader(path))
            {
                string jsonstring = fs.ReadToEnd();
                var serializerOptions = new JsonSerializerOptions();
                serializerOptions.Converters.Add(new JsonStringEnumConverter { });
                var userInformationForADays = JsonSerializer.Deserialize<List<UserInformationForADay>>(jsonstring, serializerOptions);                
            }
        }
    }
}