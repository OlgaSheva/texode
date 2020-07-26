using System;
using System.Windows;

namespace WpfFit.ErrorHandler
{
    public class DefaultErrorHandler : IErrorHandler
    {
        public void HandleError(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        public void HandleError(Exception ex, string message)
        {
            MessageBox.Show(message);
        }
    }
}
