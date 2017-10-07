// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BerasApp.Helpers
{
  public static class Settings
  {
        public static readonly string AppURL = "https://appbeer.azurewebsites.net/";

        private static ISettings AppSettings => CrossSettings.Current;

        const string UserIdKey = "userid";
        static readonly string UserIdDefault = string.Empty;

        const string AuthTokenKey = "authtoken";
        static readonly string AuthTokenDefault = string.Empty;

        public static string AuthToken
        {
            get => AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault);
            set => AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value);
        }

        public static string UserId
        {
            get => AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault);
            set => AppSettings.AddOrUpdateValue<string>(UserIdKey, value);
        }

        public static bool IsLogged => !string.IsNullOrWhiteSpace(UserId);
    }
}