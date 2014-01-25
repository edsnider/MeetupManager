using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Services;
using MeetupManager.WP8.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MeetupManager.WP8.PlatformSpecific
{
    public class WP8MeetupLogin : ILogin
    {
        #region ILogin Implementation
        WP8OAuth2Helper auth = new WP8OAuth2Helper(
            clientId: MeetupService.ClientId,
            clientSecret: MeetupService.ClientSecret,
            authorizeUrl: new Uri("https://secure.meetup.com/oauth2/authorize"),
            redirectUrl: new Uri("http://www.refractored.com/login_success.html"),
            tokenUrl: new Uri("https://secure.meetup.com/oauth2/access")
            );

        public void LoginAsync(Action<bool> loginCallback)
        {
            auth.Authenticate();

            auth.Completed += (s, ee) =>
            {
                if (ee.IsAuthenticated)
                {
                    Settings.AccessToken = ee.AuthProperties["access_token"];
                    Settings.RefreshToken = ee.AuthProperties["refresh_token"];

                    long time = 0;
                    long.TryParse(ee.AuthProperties["expires_in"], out time);

                    var nextTime = DateTime.UtcNow.AddSeconds(time).Ticks;
                    Settings.KeyValidUntil = nextTime;
                }

                if (loginCallback != null)
                    loginCallback(ee.IsAuthenticated);
            };
        }

        #endregion

    }
}
