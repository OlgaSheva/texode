using System;

namespace WpfFit.ErrorHandler
{
    public interface IErrorHandler
    {
        public void HandleError(Exception ex);

        public void HandleError(Exception ex, string message);
    }
}
