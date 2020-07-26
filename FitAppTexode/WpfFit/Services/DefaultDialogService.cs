using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace WpfFit.Services
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public string[] FilePaths { get; set; }

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

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
