using BasarnasApp.Shared.Models;
using BasarnasMobilaApp.Services;
using System.Collections.ObjectModel;

namespace BasarnasMobilaApp.Pages;

public partial class LaporanPage : ContentPage
{
    public LaporanPage()
    {
        InitializeComponent();
        BindingContext = new LaporanViewModel();


    }


}

public class LaporanViewModel : BaseViewModel
{
    public LaporanViewModel()
    {
        LoadCommand = new Command(async () => await Load());
        MapLinkCommand = new Command(async (x)=>await MapLinkAction(x));
        ShowPhotoCommand = new Command(async(x)=>await ShowPhotoAction(x));
        LoadCommand.Execute(null);
    }

    private async Task ShowPhotoAction(object obj)
    {
       
        var kejadian = (KejadianRequest)obj;
        var page = new PhotoViewPage($"/images/kejadian/{kejadian.Photo}");

        //await Share.Default.RequestAsync(new ShareTextRequest
        //{
        //    Text = "Ini Text Dari Mana kek",
        //    Title = "Share Text"    , 
        //    Uri= $"{Helper.Url}/images/kejadian/{kejadian.Photo}"
        //})
        //;


        await Shell.Current.Navigation.PushModalAsync(page);
    }

    private async Task MapLinkAction(object obj)
    {
        var kejadian = (KejadianRequest)obj;
        var latlong = kejadian.LongLat.Replace(",",".").Split(";");
        await Launcher.OpenAsync($"http://www.google.com/maps/place/{latlong[0]},{latlong[1]}");
    }

    public ObservableCollection<KejadianRequest> Kejadians { get; set; }
        = new ObservableCollection<KejadianRequest>();
    public Command LoadCommand { get; private set; }
    public Command MapLinkCommand { get; }
    public Command ShowPhotoCommand { get; }

    private async Task Load()
    {
        try
        {
            IsBusy = true;
            var service = Helper.GetService<IKejadianService>();
            var dataKejadian = await service.GetAsync();
            Kejadians.Clear();
            foreach (var item in dataKejadian.OrderByDescending(x => x.Tanggal))
            {
                Kejadians.Add(item);
            }
        }
        catch (Exception e)
        {
            await Helper.ShellMessageShow("Error", e.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}