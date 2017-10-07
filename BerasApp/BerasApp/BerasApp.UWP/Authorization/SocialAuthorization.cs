using BerasApp.Authentication;
using BerasApp.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BerasApp.UWP.Authorization
{
    public class SocialAuthorization : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(provider);

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
            client.LogoutAsync();
        }
    }
}
