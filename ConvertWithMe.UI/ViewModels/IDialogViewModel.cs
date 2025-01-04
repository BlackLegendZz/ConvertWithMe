using ConvertWithMe.UI.Models;

namespace ConvertWithMe.UI.ViewModels
{
    public interface IDialogViewModel<TData> where TData : IDialogData
    {
        public void Initialize(TData data, TaskCompletionSource tcs);
    }
}
