using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfFit.Models
{
    /// <summary>
    /// User information for the application table.
    /// </summary>
    public class User : BaseVM
    {
        /// <summary>
        /// User full name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Average number of steps taken over the entire period.
        /// </summary>
        public int AverageStepsNumber { get; private set; }

        /// <summary>
        /// The best result for the entire period.
        /// </summary>
        public int TheBestResult { get; private set; }

        /// <summary>
        /// The worst result for the entire period.
        /// </summary>
        public int TheWorstResult { get; private set; }

        /// <summary>
        /// User data for the entire period ( <day, user information for a day> ).
        /// </summary>
        public ObservableCollection<KeyValuePair<int, UserInformationForADay>> UserData { get; set; }
            = new ObservableCollection<KeyValuePair<int, UserInformationForADay>>();
    }
}
