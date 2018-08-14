using System;

namespace SB.Interfaces
{
    public interface IExceptionHandler
    {
        string GetMessage(Exception ex);
    }
}
