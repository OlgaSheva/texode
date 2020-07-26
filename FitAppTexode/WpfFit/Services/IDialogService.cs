namespace WpfFit.Services
{
    /// <summary>
    /// Dialog service.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// The file extension.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Path to the selected file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Paths to the selected files.
        /// </summary>
        public string[] FilePaths { get; set; }

        /// <summary>
        /// Show message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string message);

        /// <summary>
        /// Open files.
        /// </summary>
        /// <returns>Operation successful.</returns>
        public bool OpenFileDialog();

        /// <summary>
        /// Save file.
        /// </summary>
        /// <returns>Operation successful</returns>
        public bool SaveFileDialog();
    }
}
