using BasarnasApp.Shared.Models;
using BasarnasMobilaApp.Models;
using BasarnasMobilaApp.Services;
using FluentValidation;
using System.Windows.Input;

namespace BasarnasMobilaApp.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
	}
}

public class RegisterViewModel : BaseViewModel
{

    public Pelapor Model { get; set; } = new Pelapor(); 

    public RegisterViewModel()
    {
        validator = new RegisterValidator();
        GotoLoginCommand = new Command(GotoLoginAction);
        RegisterCommand = new Command(RegisterAction, RegiterValidate);
        Model.PropertyChanged += (s, p) => { 
                RegisterCommand = new Command(RegisterAction, RegiterValidate);
        };
    }

    private async void RegisterAction(object obj)
    {
        try
        {
            var service = Helper.GetService<IPelaporService>();
            var result = await service.PostAsync(Model);
            if (result != null)
            {
                GotoLoginAction(null);
            }
            else
            {
                throw new SystemException("User Tidak Berhasil Dibuat !");
            }
        }
        catch (Exception ex)
        {
           await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private bool RegiterValidate(object arg)
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

    private RegisterValidator validator;

    public ICommand GotoLoginCommand { get; set; }


    private ICommand registerCommand;

    public ICommand RegisterCommand
    {
        get { return registerCommand; }
        set { SetProperty(ref registerCommand , value); }
    }



}

public class RegisterValidator : AbstractValidator<Pelapor>
{
    public RegisterValidator()
    {
        RuleFor((x) => x.Name).NotEmpty().WithMessage("nama Tidak Boleh Kosong!");
        RuleFor((x) => x.Email).NotEmpty().WithMessage("User Name Tidak Boleh Kosong")
            .EmailAddress().WithMessage("Masukkan Email Anda !");
        RuleFor((x) => x.Address).NotEmpty().WithMessage("Alamat tidak boleh kosong !");
        RuleFor((x) => x.Password).NotEmpty().WithMessage("Password Tidak Boleh Kosong!");
        RuleFor((x) => x.PhoneNumber).NotEmpty().WithMessage("Nomor HP Tidak Boleh Kosong");
        RuleFor((x) => x.ConfirmPassoword)
            .NotEmpty().WithMessage("Confirm Password Tidak Boleh Kosong! !")
            .NotEqual(x=>x.Password).WithMessage("Password Harus Sama");
    }
}