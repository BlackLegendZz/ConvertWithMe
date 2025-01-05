using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConvertWithMe.UI.Models;

namespace ConvertWithMe.UI.ViewModels
{
    public partial class NotificationDialogViewModel : ObservableObject, IDialogViewModel<NotificationViewModelData, Confirmation>
    {
        [ObservableProperty]
        private string windowTitle = string.Empty;
        [ObservableProperty]
        private string message = string.Empty;
        private TaskCompletionSource<Confirmation>? tcs;

        public void Initialize(NotificationViewModelData data, TaskCompletionSource<Confirmation> tcs)
        {
            this.tcs = tcs;
            WindowTitle = data.Title;
            Message = data.Message;
        }

        [RelayCommand]
        public void ButtonClicked()
        {
            if (tcs != null)
            {
                tcs.SetResult(Confirmation.Ok);
            }
        }
    }
}
