using System.Collections.Generic;
using System.Threading.Tasks;
using WpfFit.Models;

namespace WpfFit.Readers
{
    /// <summary>
    /// File reader.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Read all files from given directory and get list of objects.
        /// </summary>
        /// <param name="targetDirectory">Target directory.</param>
        /// <returns>A <see cref="Task{IDictionary{int,IList{UserInformationForADay}}}">.</returns>
        Task<IDictionary<int, IList<UserInformationForADay>>> ReadDirectory(string targetDirectory = null);

        /// <summary>
        /// Read all files and get list of objects.
        /// </summary>
        /// <param name="files">Files.</param>
        /// <returns>A <see cref="Task{IDictionary{int,IList{UserInformationForADay}}}">.</returns>
        Task<IDictionary<int, IList<UserInformationForADay>>> ReadFiles(string[] files);

        /// <summary>
        /// Read file and get list of objects.
        /// </summary>
        /// <returns>A <see cref="Task{IList{UserInformationForADay}}">.</returns>
        Task<IList<UserInformationForADay>> ReadFile(string path);
    }
}