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
    internal class UserXmlWriter : IUserWriter
    {
        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserXmlWriter"/> class.
        /// </summary>
        /// <param name="writer">The file writer.</param>
        /// <exception cref="ArgumentNullException">Writer is null.</exception>
        public UserXmlWriter(StreamWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public async Task Write(User user)
        {
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

            await doc.SaveAsync(_writer, SaveOptions.None, default(CancellationToken));
        }
    }
}
