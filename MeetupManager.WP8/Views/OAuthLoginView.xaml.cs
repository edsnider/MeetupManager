using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MeetupManager.WP8.Helpers;

namespace MeetupManager.WP8.Views
{
    public partial class OAuthLoginView : PhoneApplicationPage
    {
        private string _redirectUrl;

        public OAuthLoginView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            IDictionary<string, string> parameters = this.NavigationContext.QueryString;

            string authorizeUrl = parameters["authUrl"];
            string clientId = parameters["clientId"];
            string redirectUrl = parameters["redirectUrl"];
            this._redirectUrl = redirectUrl; // needed in the Navigated event handler method below

            string authuri = string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}", authorizeUrl, clientId, redirectUrl);

            browser_login.Navigate(new Uri(authuri, UriKind.Absolute));
        }

        private void browser_login_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (browser_login.Source != null && browser_login.Source.OriginalString.StartsWith(this._redirectUrl))
            {
                var query = QueryStringHelper.ParseQueryString(browser_login.Source.Query);

                if (query.ContainsKey("code"))
                {
                    var code = query["code"];
                    PhoneApplicationService.Current.State["MeetupManager.WP8.AuthCode"] = code;

                    NavigationService.GoBack();
                }
            }
        }
    }
}