using ConvertWithMe.UI.Models;

namespace ConvertWithMe.UI.ViewModels
{
    public interface IDialogViewModel<TData, TResponse> where TData : IDialogData where TResponse : Enum
    {
        public void Initialize(TData data, TaskCompletionSource<TResponse> tcs);
    }
}
