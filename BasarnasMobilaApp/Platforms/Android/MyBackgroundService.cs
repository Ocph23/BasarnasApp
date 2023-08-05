using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using BasarnasMobilaApp;
using Microsoft.AspNetCore.SignalR.Client;

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
            .SetSmallIcon(Resource.Mipmap.appicon)
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

            hubConnection.On<string>("ReceiveMessage", (message) =>
            {
                // Display the message in a notification
                BadgeNumber++;
                notification.SetNumber(BadgeNumber);
                notification.SetContentTitle(message);
                StartForeground(myId, notification.Build());
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
}