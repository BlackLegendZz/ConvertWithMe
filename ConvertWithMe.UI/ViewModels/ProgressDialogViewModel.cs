using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConvertWithMe.UI.Models;

namespace ConvertWithMe.UI.ViewModels
{
    public partial class ProgressDialogViewModel : ObservableObject, IDialogViewModel<ProgressViewModelData, Confirmation>
    {

        [ObservableProperty]
        private string windowTitle = string.Empty;

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private string progressMessage = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DoneDownloading))]
        private float progress = 0;

        public bool DoneDownloading => Progress == 100f;

        private TaskCompletionSource<Confirmation>? tcs;
        public void Initialize(ProgressViewModelData data, TaskCompletionSource<Confirmation> tcs)
        {
            this.tcs = tcs;
            WindowTitle = data.WindowTitle;
            Message = data.Message;
        }

        [RelayCommand]
        public void ButtonOkClicked()
        {
            if (tcs != null)
            {
                tcs.SetResult(Confirmation.Ok);
            }
        }
    }
}
