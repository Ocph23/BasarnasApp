using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using BasarnasMobilaApp;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasMobilaApp.Platforms.Android;

[Service]
public class ForegroundService : Service
{
    private string NOTIFICATION_CHANNEL_ID = "1000";
    private int NOTIFICATION_ID = 1;
    private string NOTIFICATION_CHANNEL_NAME = "notification";
    private string NOTIFICATION_GROUP_NAME = "com.ocph23.basarnasmobileapp";
    private HubConnection hubConnection;
    public ForegroundService()
    {
            
    }

    private void startForegroundService(string message)
    {
        var notifcationManager = GetSystemService(Context.NotificationService) as NotificationManager;

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            createNotificationChannel(notifcationManager);
        }

        var notification = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
        notification.SetAutoCancel(false);
        notification.SetOngoing(true);
        notification.SetSmallIcon(Resource.Mipmap.appicon);
        notification.SetContentTitle("Basarnas App");
        notification.SetContentText(message);
        notification.SetGroup(NOTIFICATION_GROUP_NAME);
        notification.SetStyle(new NotificationCompat.InboxStyle().AddLine(message).AddLine("Sinpet2"));
        StartForeground(NOTIFICATION_ID, notification.Build() );
        NOTIFICATION_ID++;
    }

    private void createNotificationChannel(NotificationManager notificationMnaManager)
    {
        var channel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, NOTIFICATION_CHANNEL_NAME,
        NotificationImportance.High);
        notificationMnaManager.CreateNotificationChannel(channel);
    }

    public override IBinder OnBind(Intent intent)
    {
        return null;
    }


    public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    {

        hubConnection = new HubConnectionBuilder()
        .WithUrl(Helper.Url + "/apphub")
        .Build();
        hubConnection.On<string, string>("ReceiveMessage", async (user, message) =>
        {
            startForegroundService(message);
        });

        _=hubConnection.StartAsync();



        return StartCommandResult.NotSticky;
    }



}
