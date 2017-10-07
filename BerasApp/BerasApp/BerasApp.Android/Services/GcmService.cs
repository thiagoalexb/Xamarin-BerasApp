using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BerasApp.Helpers;
using System.Linq;

[assembly: Permission(Name = "beras.beras.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "beras.beras.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace BerasApp.Droid.Services
{

    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "beras.beras" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "beras.beras" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "beras.beras" })]

    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { "901623670509" };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        MobileServiceClient client = new MobileServiceClient(Settings.AppURL);

        public static string RegistrationID { get; private set; }

        public GcmService() : base(PushHandlerBroadcastReceiver.SENDER_IDS) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);

            RegistrationID = registrationId;

            var push = client.GetPush();

            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
        }

        public async void Register(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
        {
            try
            {
                const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}}";
                JObject templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                     {"body", templateBodyGCM}
                };

                await push.RegisterAsync(RegistrationID, templates);
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        protected override void OnError(Context context, string errorId)
        {
            throw new NotImplementedException();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();
            if (intent != null && intent.Extras != null)
            {
                foreach (var item in intent.Extras.KeySet())
                    msg.AppendLine(item + "=" + intent.Extras.Get(item).ToString());
            }

            //Store the message
            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            string message = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(message))
            {
                if (message.Contains("_"))
                {
                    var arrMsg = message.Split('_');
                    CreateNotification(arrMsg.FirstOrDefault(), arrMsg.LastOrDefault());
                    return;
                }
                CreateNotification("New Push", message);
                return;
            }

            string msg2 = intent.Extras.GetString("msg");
            if (!string.IsNullOrEmpty(msg2))
            {
                CreateNotification("New hub message", message);
                return;
            }

            CreateNotification("Unknown message details", msg.ToString());
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            throw new NotImplementedException();
        }

        private void CreateNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            //Create an intent to show UI
            var uiIntent = new Intent(this, typeof(MainActivity));

            //Use Notification builder
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            //Create the notification
            //we use the pending intent, passing our UI intent over which will get called
            //when the notification is tapped
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(desc)
                //Set the notification sound
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                //Auto cancel will remove the notification once the user touches it
                .SetAutoCancel(true).Build();

            //Show the notification
            notificationManager.Notify(1, notification);
        }
    }
}