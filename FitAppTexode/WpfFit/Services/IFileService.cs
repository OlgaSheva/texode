using System.Collections.Generic;
using System.Threading.Tasks;
using WpfFit.Models;

namespace WpfFit.Services
{
    /// <summary>
    /// File service.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Get all users statictic.
        /// </summary>
        /// <returns>A <see cref="Task{IList{User}}".</returns>
        public Task<IList<User>> GetUsersStatistic();
    }
}
