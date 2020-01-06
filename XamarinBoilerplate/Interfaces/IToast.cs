using System;
namespace XamarinBoilerplate.Interfaces
{
    public interface IToast
    {
        void ShowToastMessage(string message, bool shortDuration);
    }
}
