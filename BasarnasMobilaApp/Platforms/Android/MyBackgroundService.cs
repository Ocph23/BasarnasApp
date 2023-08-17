using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using BasarnasApp.Shared.Models;
using BasarnasMobilaApp;
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

namespace BasarnasMobilaApp.Platforms.Android;

[Service]
internal class MyBackgroundService : Service
{
    Timer timer = null;
    int myId = (new object()).GetHashCode();
    int BadgeNumber = 0;
    NotificationCompat.Builder notification;
    HubConnection hubConnection;

    public override IBinder OnBind(Intent intent)
    {
        return null;
    }

    public override StartCommandResult OnStartCommand(Intent intent,
        StartCommandFlags flags, int startId)
    {
        var input = intent.GetStringExtra("inputExtra");

        var notificationIntent = new Intent(this, typeof(MainActivity));
        var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent,
            PendingIntentFlags.Immutable);

        notification = new NotificationCompat.Builder(this,
                MainApplication.ChannelId)
            .SetContentText(input)
            .SetSmallIcon(Resource.Mipmap.basarnasicon)
            .SetContentIntent(pendingIntent);

        // Increment the BadgeNumber
        BadgeNumber++;
        // set the number
        notification.SetNumber(BadgeNumber);
        // set the title (text) to Service Running
        notification.SetContentTitle("Service Running");
        // build and notify
        StartForeground(myId, notification.Build());

        // timer to ensure hub connection
        timer = new Timer(Timer_Elapsed, notification, 0, 10000);

        // You can stop the service from inside the service by calling StopSelf();

        return StartCommandResult.Sticky;
    }

    async Task EnsureHubConnection()
    {
        if (hubConnection == null)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(Helper.Url + "/apphub")
                .Build();

            hubConnection.On<string>("KejadianMessage", (message) =>
            {
                startForegroundService(message);
            });
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                // Put a breakpoint on the next line to debug
            }

        }
        else if (hubConnection.State != HubConnectionState.Connected)
        {
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                // Put a breakpoint on the next line to debug
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    async void Timer_Elapsed(object state)
    {
        AndroidServiceManager.IsRunning = true;

        await EnsureHubConnection();
    }



    private string NOTIFICATION_CHANNEL_ID = "1000";
    private int NOTIFICATION_ID = 1;
    private string NOTIFICATION_CHANNEL_NAME = "notification";
    private string NOTIFICATION_GROUP_NAME = "com.ocph23.basarnasmobilaapp";

    private void startForegroundService(string message)
    {
        var notifcationManager = GetSystemService(Context.NotificationService) as NotificationManager;

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            createNotificationChannel(notifcationManager);
        }

        var notification = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
        string newMessage = string.Empty;
        string seccoundLine = string.Empty;
        var data = JsonSerializer.Deserialize<KejadianRequest>(message, Helper.JsonOption);
        if(data != null)
        {
            newMessage = $"Terjadi {data.JenisKejadianName} di {data.DistrictName}";
            seccoundLine = $"Dilaporkan Oleh : {seccoundLine}";
        }

        BadgeNumber++;
        notification.SetNumber(BadgeNumber);
        notification.SetAutoCancel(false);
        notification.SetOngoing(true);
        notification.SetSmallIcon(Resource.Mipmap.basarnasicon);
        notification.SetContentTitle("Laporan Baru");
        notification.SetContentText(message);
        notification.SetGroup(NOTIFICATION_GROUP_NAME);
        notification.SetStyle(new NotificationCompat.InboxStyle()
            .AddLine(newMessage)
            .AddLine(seccoundLine));
        StartForeground(NOTIFICATION_ID, notification.Build());
        NOTIFICATION_ID++;
    }
    private void createNotificationChannel(NotificationManager notificationMnaManager)
    {
        var channel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, NOTIFICATION_CHANNEL_NAME,
        NotificationImportance.High);
        notificationMnaManager.CreateNotificationChannel(channel);
    }

}