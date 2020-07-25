using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfFit.Models;

namespace WpfFit.Writers
{
    /// <summary>
    /// User XML writer.
    /// </summary>
    internal class UserXmlWriter
    {
        private readonly IConfiguration _configuration;
        private readonly string _directory;

        public UserXmlWriter(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _directory = _configuration.GetSection("Directory").Value;
        }

        public async Task Write(User user)
        {
            using StreamWriter writer = new StreamWriter(File.Create($"{_directory}\\{user.UserName}.xml"));
            XElement userInfo = new XElement("user");
            var doc = new XDocument(
               new XDeclaration("1.0", "utf-16", "yes"),
               userInfo);
            userInfo.Add(
                new XElement("User", user.UserName),
                new XElement("AverageStepsNumber", user.AverageStepsNumber),
                new XElement("TheBestResult", user.TheBestResult),
                new XElement("TheWorstResult", user.TheWorstResult));

            var userData = new XElement("UserData");
            foreach (var item in user.UserData)
            {
                var dayInformation = new XElement("Day");
                dayInformation.Add(
                    new XAttribute("Day", item.Key),
                    new XElement("Rank", item.Value.Rank),
                    new XElement("Status", item.Value.Status),
                    new XElement("Steps", item.Value.Steps));
                userData.Add(dayInformation);
            }
            userInfo.Add(userData);

            await doc.SaveAsync(writer, SaveOptions.None, default(CancellationToken));
        }
    }
}
