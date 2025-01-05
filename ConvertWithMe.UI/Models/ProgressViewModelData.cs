namespace ConvertWithMe.UI.Models
{
    public class ProgressViewModelData : IDialogData
    {
        public string WindowTitle = string.Empty;
        public string Message = string.Empty;
        public float Progress = 0;

        public ProgressViewModelData(string title, string message)
        {
            WindowTitle = title;
            Message = message;
        }
    }
}
