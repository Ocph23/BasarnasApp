using BasarnasApp.Shared.Models;
using BasarnasMobilaApp.Models;
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

public class LaporanViewModel      :BaseViewModel
{
    public LaporanViewModel()
    {
        LoadCommand = new Command(async ()=> await Load());
        LoadCommand.Execute(null);
    }
    public ObservableCollection<KejadianRequest> Kejadians { get; set; } = new ObservableCollection<KejadianRequest>();
    public Command LoadCommand { get; private set; }

    private async Task Load()
    {
        try
        {
            IsBusy = true;
            var service = Helper.GetService<IKejadianService>();
            var dataKejadian = await service.GetAsync();
            foreach (var item in dataKejadian)
            {
                Kejadians.Add(item);
            }
        }
        catch (Exception e)
        {
            await Helper.ShellMessageShow("Error",e.Message);
        }
        finally
        {
            IsBusy=false;
        }
    }
}