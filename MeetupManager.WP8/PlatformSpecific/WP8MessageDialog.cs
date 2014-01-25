﻿using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MeetupManager.WP8.PlatformSpecific
{
    public class WP8MessageDialog : IMessageDialog
    {
        public void SendMessage(string message, string title = null)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(message, title, MessageBoxButton.OK);
            });
        }

        public void SendToast(string message)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(message);
            });
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
