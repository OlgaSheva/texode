using Newtonsoft.Json;

namespace WpfFit.Models
{
    /// <summary>
    /// User information for the day.
    /// </summary>
    public class UserInformationForADay
    {
        /// <summary>
        /// User rank on a day.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// User full name.
        /// </summary>
        [JsonIgnore]
        public string User { get; set; }

        /// <summary>
        /// User status on a day.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Number  of steps on a day.
        /// </summary>
        public int Steps { get; set; }
    }
}
