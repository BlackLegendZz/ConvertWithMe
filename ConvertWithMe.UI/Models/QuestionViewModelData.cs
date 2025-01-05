namespace ConvertWithMe.UI.Models
{
    public class QuestionViewModelData : IDialogData
    {
        public string Title = string.Empty;
        public string Question = string.Empty;

        public QuestionViewModelData(string title, string question)
        {
            Title = title;
            Question = question;
        }
    }
}
