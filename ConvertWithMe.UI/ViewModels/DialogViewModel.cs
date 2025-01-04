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

        public async Task ShowDialogAsync<TData>(IDialogViewModel<TData> viewModel, TData data) where TData : IDialogData
        {
            IsVisible = true;

            TaskCompletionSource tcs = new TaskCompletionSource();
            viewModel.Initialize(data, tcs);

            ViewModel = viewModel;

            await tcs.Task.WaitAsync(CancellationToken.None);

            IsVisible = false;
        }
    }
}
