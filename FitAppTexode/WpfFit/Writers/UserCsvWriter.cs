using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WpfFit.Models;

namespace WpfFit.Writers
{
    /// <summary>
    /// User CSV writer.
    /// </summary>
    public class UserCsvWriter : IUserWriter
    {
        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCsvWriter"/> class.
        /// </summary>
        /// <param name="writer">The file writer.</param>
        /// <exception cref="ArgumentNullException">Writer is null.</exception>
        public UserCsvWriter(StreamWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public async Task Write(User user)
        {
            var builder = new StringBuilder();
            builder.Append($"{user.UserName},");
            builder.Append($"{user.AverageStepsNumber},");
            builder.Append($"{user.TheBestResult},");
            builder.AppendLine($"{user.TheWorstResult},");

            foreach (var day in user.UserData)
            {
                builder.AppendLine($"{day.Key};{day.Value.Rank},{day.Value.Status},{day.Value.Steps}");
            }

            await _writer.WriteLineAsync(builder.ToString());
        }
    }
}
