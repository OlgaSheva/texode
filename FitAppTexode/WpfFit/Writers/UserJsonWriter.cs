using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using WpfFit.Models;

namespace WpfFit.Writers
{
    /// <summary>
    /// User JSON writer.
    /// </summary>
    internal class UserJsonWriter : IUserWriter
    {
        private readonly StreamWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserJsonWriter"/> class.
        /// </summary>
        /// <param name="writer">The file writer.</param>
        /// <exception cref="ArgumentNullException">Writer is null.</exception>
        public UserJsonWriter(StreamWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <inheritdoc/>
        public async Task Write(User user)
        {
            JsonSerializer serializer = new JsonSerializer { Formatting = Formatting.Indented };
            serializer.Serialize(_writer, user);
        }
    }
}
