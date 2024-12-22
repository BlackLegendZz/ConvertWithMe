using CommunityToolkit.Mvvm.Messaging.Messages;
using ConvertWithMe.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertWithMe.UI.Messengers
{
    public class TransferSettingsMessage : ValueChangedMessage<FileItem?>
    {
        public TransferSettingsMessage(FileItem? value) : base(value)
    {
    }
}
}
