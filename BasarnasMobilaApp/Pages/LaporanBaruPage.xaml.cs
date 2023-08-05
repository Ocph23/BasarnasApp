using BasarnasApp.Shared.Models;
using BasarnasMobilaApp.Models;
using BasarnasMobilaApp.Services;
using FluentValidation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BasarnasMobilaApp.Pages;

public partial class LaporanBaruPage : ContentPage
{
    public LaporanBaruPage()
    {
        InitializeComponent();
        BindingContext = new LaporanBaruViewModel();
    }
}

public class LaporanBaruViewModel : BaseViewModel
{
    private Kejadian model;

    public Kejadian Model
    {
        get { return model; }
        set { SetProperty(ref model , value); }
    }


    private Command saveComman;

    public Command SaveCommand
    {
        get { return saveComman; }
        set { SetProperty(ref saveComman, value); }
    }


    public LaporanBaruViewModel()
    {
        Model = new Kejadian();
        _ = Load();

        TakePhotoCommand = new Command(async() => await TakePhoto());

        Model.PropertyChanged += (s, p) =>
        {
            SaveCommand = new Command(SaveAction, SaveValidate);

        };
    }

    private bool SaveValidate(object arg)
    {
        if (IsBusy)
            return false;
        var validator = new LaporanBaruValidation();
        return validator.Validate(Model).IsValid;
    }

    private async void SaveAction(object obj)
    {
        try
        {
            var kejadianservice = Helper.GetService<IKejadianService>();
            var req = new KejadianRequest { DistrictId = Model.District.Id, Id=Model.Id, 
                JenisKejadianId=Model.JenisKejadian.Id, LongLat=Model.LongLat,  PelaporId=Model.Pelapor.Id,
                Tanggal=DateTime.UtcNow,  DataPhoto=Model.DatPhoto};
            var result = await kejadianservice.PostAsync(req);
            if(result!=null)
            {
               await Helper.ShellMessageShow("Success", "Laporan Berhasil Dibuat !");
               Model = new Kejadian();
               Photo = ImageSource.FromFile("noimage.jpg");
               await Shell.Current.GoToAsync("//LaporanPage");
            }

        }
        catch (Exception ex)
        {
           await Helper.ShellMessageShow("Error",ex.Message);
        }
    }

    private ImageSource photo =ImageSource.FromFile("noimage.jpg");

    public ImageSource Photo
    {
        get { return photo; }
        set { SetProperty(ref photo , value); }
    }


    public async Task TakePhoto()
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var result = await Helper.TakePhoto();
                if (result != null)
                {
                    Model.DatPhoto = result;
                    Photo = ImageSource.FromStream(() => new MemoryStream(Model.DatPhoto));
                }
            }
        }
        catch (Exception ex)
        {
           await Helper.ShellMessageShow("Error", ex.Message);
        }
    }



    public ObservableCollection<DistrictRequest> Districts { get; set; } = new ObservableCollection<DistrictRequest>();
    public ObservableCollection<JenisKejadianRequest> JenisKejadians { get; set; } = new ObservableCollection<JenisKejadianRequest>();
    public Command TakePhotoCommand { get; }

    private async Task Load()
    {
        try
        {
            IsBusy = true;
            Model.Pelapor = Account.GetProfile();
            var districtService = Helper.GetService<IDistrictService>();
            var jeniskejadianService = Helper.GetService<IJenisKejadianService>();

            var districs = await districtService.GetAsync();
            Districts.Clear();
            foreach (var item in districs)
            {
                Districts.Add(item);
            }

            var jeniskejadians = await jeniskejadianService.GetAsync();
            JenisKejadians.Clear();
            foreach (var item in jeniskejadians)
            {
                JenisKejadians.Add(item);
            }
       
            await GetCurrentLocation();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
        }
        finally { IsBusy = false; }
    }

    private CancellationTokenSource _cancelTokenSource;
    private Location location;
    private bool _isCheckingLocation;

    [Obsolete]
    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
                Device.BeginInvokeOnMainThread(() => { 
                    Model.LongLat = $"{location.Latitude.ToString()}; {location.Longitude.ToString()}";
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                
                });
            }
        }

        catch (Exception ex)
        {
            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}

internal class LaporanBaruValidation : AbstractValidator<Kejadian>
{
    public LaporanBaruValidation()
    {
        RuleFor((x) => x.District).NotEmpty().WithMessage("Distrik Tidak Boleh Kosong !");
        RuleFor((x) => x.JenisKejadian).NotEmpty().WithMessage("Jenis Kejadian Tidak Boleh Kosong !");
        RuleFor((x) => x.Description).NotEmpty().WithMessage("Deskripsi Tidak Boleh Kosong !");
        RuleFor((x) => x.Pelapor).NotEmpty().WithMessage("Pelapor Tidak Boleh Kosong !");
        RuleFor((x) => x.DatPhoto).NotEmpty().WithMessage("Photo Tidak Boleh Kosong !");
    }
}