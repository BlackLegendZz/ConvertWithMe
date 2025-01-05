using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConvertWithMe.UI.Models;

namespace ConvertWithMe.UI.ViewModels
{
    public partial class QuestionDialogViewModel : ObservableObject, IDialogViewModel<QuestionViewModelData, Question>
    {
        [ObservableProperty]
        private string windowTitle = string.Empty;
        [ObservableProperty]
        private string question = string.Empty;
        private TaskCompletionSource<Question>? tcs;

        public void Initialize(QuestionViewModelData data, TaskCompletionSource<Question> tcs)
        {
            WindowTitle = data.Title;
            Question = data.Question;
            this.tcs = tcs;
        }

        [RelayCommand]
        public void ButtonYesClicked()
        {
            if (tcs != null)
            {
                tcs.SetResult(UI.Question.Yes);
            }
        }

        [RelayCommand]
        public void ButtonNoClicked()
        {
            if (tcs != null)
            {
                tcs.SetResult(UI.Question.No);
            }
        }
    }
}
