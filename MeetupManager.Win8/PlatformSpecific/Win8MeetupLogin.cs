using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupManager.Win8.PlatformSpecific
{
    public class Win8MeetupLogin : ILogin
    {
        public void LoginAsync(Action<bool> loginCallback)
        {
            loginCallback(true);
        }
    }
}
