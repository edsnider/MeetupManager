using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MeetupManager.Win8.PlatformSpecific
{
    public class Win8MessageDialog : IMessageDialog
    {
        public async void SendMessage(string message, string title = null)
        {
            var dialog = new MessageDialog(message, title);
            await dialog.ShowAsync();
        }

        public async void SendToast(string message)
        {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        public void SendConfirmation(string message, string title, Action<bool> confirmationAction)
        {
            throw new NotImplementedException();
        }

        public void AskForString(string message, string title, Action<string> returnString)
        {
            throw new NotImplementedException();
        }
    }
}
