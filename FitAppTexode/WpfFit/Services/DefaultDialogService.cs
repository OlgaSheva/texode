using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace WpfFit.Services
{
    /// <summary>
    /// Default file dialog service.
    /// </summary>
    public class DefaultDialogService : IDialogService
    {
        /// <inheritdoc/>
        public string FilePath { get; set; }

        /// <inheritdoc/>
        public string FileExtension { get; set; }

        /// <inheritdoc/>
        public string[] FilePaths { get; set; }

        /// <inheritdoc/>
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "JSON (*.json)|*.json",
                DefaultExt = "json"
            };            
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                FilePaths = openFileDialog.FileNames;
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "XML (*.xml)|*.xml|JSON (*.json)|*.json|CSV (*.csv)|*.csv",
                DefaultExt = "xml"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                FileExtension = Path.GetExtension(saveFileDialog.FileName);
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
