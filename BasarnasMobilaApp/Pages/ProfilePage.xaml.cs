using BasarnasMobilaApp.Models;
using BasarnasMobilaApp.Services;
using FluentValidation;
using System.Windows.Input;

namespace BasarnasMobilaApp.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}
}

public class ProfileViewModel : BaseViewModel
{

    public Pelapor Model { get; set; } = new Pelapor();

    public ProfileViewModel()
    {
        validator = new ProfileValidator();
        UpdateProfileCommand = new Command(ProfileAction, ProfileValidate);
        Model.PropertyChanged += (s, p) => {
            UpdateProfileCommand = new Command(ProfileAction, ProfileValidate);
        };
    }

    private async void ProfileAction(object obj)
    {
        try
        {
            var service = Helper.GetService<IPelaporService>();
            var result = await service.PostAsync(Model);
            if (result != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Data Berhasil Disimpan !", "Ok");
            }
            else
            {
                throw new SystemException("User Tidak Berhasil Dibuat !");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private bool ProfileValidate(object arg)
    {
        return validator.Validate(Model).IsValid;
    }

    private void GotoLoginAction(object obj)
    {
        Application.Current.MainPage = Helper.GetService<LoginPage>();
    }

    private string userName;

    public string UserName
    {
        get { return userName; }
        set { SetProperty(ref userName, value); }
    }


    private string password;

    public string Password
    {
        get { return password; }
        set { SetProperty(ref password, value); }
    }

    private ProfileValidator validator;

    public ICommand GotoLoginCommand { get; set; }


    private ICommand updateProfileCommand;

    public ICommand UpdateProfileCommand
    {
        get { return updateProfileCommand; }
        set { SetProperty(ref updateProfileCommand, value); }
    }



}

public class ProfileValidator : AbstractValidator<Pelapor>
{
    public ProfileValidator()
    {
        RuleFor((x) => x.Name).NotEmpty().WithMessage("nama Tidak Boleh Kosong!");
        RuleFor((x) => x.Email).NotEmpty().WithMessage("User Name Tidak Boleh Kosong")
            .EmailAddress().WithMessage("Masukkan Email Anda !");
        RuleFor((x) => x.Address).NotEmpty().WithMessage("Alamat tidak boleh kosong !");
        RuleFor((x) => x.PhoneNumber).NotEmpty().WithMessage("Nomor HP Tidak Boleh Kosong");
      
    }
}