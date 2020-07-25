using System;

namespace WpfFit.AsyncCommand
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
