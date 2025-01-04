namespace ConvertWithMe.UI.Models
{
    public class NotificationViewModelData : IDialogData
    {
        public string Title = string.Empty;
        public string Message = string.Empty;

        public NotificationViewModelData(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
