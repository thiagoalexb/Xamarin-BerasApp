using BerasApp.Authentication;
using BerasApp.Helpers;
using BerasApp.iOS.Authentication;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthorization))]
namespace BerasApp.iOS.Authentication
{
    public class SocialAuthorization : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var current = GetController();
                var user = await client.LoginAsync(current, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LogoutAsync(MobileServiceClient client)
        {
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }

            client.LogoutAsync();
        }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null) return null;

            var current = root;
            while(current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }

    }
}
