using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace MeetupManager.WP8.Helpers
{
    public class WP8OAuth2Helper
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly Uri _authorizeUrl;
        private readonly Uri _redirectUrl;
        private readonly Uri _tokenUrl;
        private readonly PhoneApplicationFrame _frame;

        private bool _isComplete;

        public event EventHandler<AuthCompletedEventArgs> Completed;
        public event EventHandler<AuthErrorEventArgs> Error;

        public WP8OAuth2Helper(string clientId, string clientSecret, Uri authorizeUrl, Uri redirectUrl, Uri tokenUrl)
        {
            this._clientId = clientId;
            this._clientSecret = clientSecret;
            this._authorizeUrl = authorizeUrl;
            this._redirectUrl = redirectUrl;
            this._tokenUrl = tokenUrl;
            this._frame = Application.Current.RootVisual as PhoneApplicationFrame;
        }

        public async void Authenticate()
        {
            string loginViewUri = string.Format("/Views/OAuthLoginView.xaml?authUrl={0}&clientId={1}&redirectUrl={2}", this._authorizeUrl, this._clientId, this._redirectUrl.AbsoluteUri);

            SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);

            Observable.FromEvent<NavigatingCancelEventHandler, NavigatingCancelEventArgs>(
                h => new NavigatingCancelEventHandler(h),
                h => this._frame.Navigating += h,
                h => this._frame.Navigating -= h)
                    .SkipWhile(h => h.EventArgs.NavigationMode != NavigationMode.Back)
                    .Take(1)
                    .Subscribe(e => semaphore.Release());

            this._frame.Navigate(new Uri(loginViewUri, UriKind.Relative));

            await semaphore.WaitAsync();

            string authCode = string.Empty;
            if (PhoneApplicationService.Current.State.ContainsKey("MeetupManager.WP8.AuthCode"))
            {
                authCode = (string)PhoneApplicationService.Current.State["MeetupManager.WP8.AuthCode"];

                RequestAccessTokenAsync(authCode).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                        OnError(task.Exception);
                    else
                        OnSuccess(task.Result);
                });
            }
        }

        private async Task<IDictionary<string, string>> RequestAccessTokenAsync(string authCode)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("grant_type", "authorization_code");
            query.Add("code", authCode);
            query.Add("redirect_uri", this._redirectUrl.AbsoluteUri);
            query.Add("client_id", this._clientId);
            if (!string.IsNullOrEmpty(this._clientSecret))
                query.Add("client_secret", this._clientSecret);

            string queryString = QueryStringHelper.BuildQueryString(query);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this._tokenUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] buffer = Encoding.UTF8.GetBytes(queryString);
            request.ContentLength = buffer.Length;

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                stream.Write(buffer, 0, buffer.Length);
            }

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<IDictionary<string, string>>(result);
                }
            }
        }

        public void OnSuccess(IDictionary<string, string> props)
        {
            if (this._isComplete)
                return;

            this._isComplete = true;

            var completed = Completed;
            if (completed != null)
                completed(this, new AuthCompletedEventArgs(props));
        }

        public void OnError(Exception exception)
        {
            var error = Error;
            if (error != null)
                error(this, new AuthErrorEventArgs(exception));
        }
    }

    public class AuthCompletedEventArgs : EventArgs
    {
        public bool IsAuthenticated { get { return this.AuthProperties != null; } }

        public IDictionary<string, string> AuthProperties { get; private set; }

        public AuthCompletedEventArgs(IDictionary<string, string> authProperties)
        {
            this.AuthProperties = authProperties;
        }
    }

    public class AuthErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public AuthErrorEventArgs(Exception e)
        {
            this.Exception = e;
        }

        public AuthErrorEventArgs(string message)
        {
            this.Exception = new Exception(message);
        }
    }
}
