using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WpfFit.Models;

namespace WpfFit.Writers
{
    public class UserCsvWriter
    {
        private readonly IConfiguration _configuration;
        private readonly string _directory;

        public UserCsvWriter(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _directory = _configuration.GetSection("Directory").Value;
        }

        public async Task Write(User user)
        {
            using StreamWriter writer = new StreamWriter(File.Create($"{_directory}\\{user.UserName}.csv"), Encoding.UTF8);
            var builder = new StringBuilder();
            builder.Append($"{user.UserName},");
            builder.Append($"{user.AverageStepsNumber},");
            builder.Append($"{user.TheBestResult},");
            builder.AppendLine($"{user.TheWorstResult},");

            foreach (var day in user.UserData)
            {
                builder.AppendLine($"{day.Key};{day.Value.Rank},{day.Value.Status},{day.Value.Steps}");
            }

            await writer.WriteLineAsync(builder.ToString());
        }
    }
}
