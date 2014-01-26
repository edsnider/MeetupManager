using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupManager.Win8.PlatformSpecific
{
    public class Win8MessageDialog : IMessageDialog
    {
        public void SendMessage(string message, string title = null)
        {
            throw new NotImplementedException();
        }

        public void SendToast(string message)
        {
            throw new NotImplementedException();
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
