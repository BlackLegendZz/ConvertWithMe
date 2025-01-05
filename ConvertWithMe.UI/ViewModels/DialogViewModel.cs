using CommunityToolkit.Mvvm.ComponentModel;
using ConvertWithMe.UI.Models;

namespace ConvertWithMe.UI.ViewModels
{
    public partial class DialogViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isVisible = false;
        [ObservableProperty]
        private object? viewModel;

        public async Task<TResponse> ShowDialogAsync<TData, TResponse>(IDialogViewModel<TData, TResponse> viewModel, TData data) where TData : IDialogData where TResponse : Enum
        {
            IsVisible = true;

            TaskCompletionSource<TResponse> tcs = new TaskCompletionSource<TResponse>();
            viewModel.Initialize(data, tcs);

            ViewModel = viewModel;

            TResponse result = await tcs.Task.WaitAsync(CancellationToken.None);

            IsVisible = false;
            return result;
        }
    }
}
