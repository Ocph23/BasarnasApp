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
        IsPasswordCommand = new Command(IsPasswordAction);
        Model.PropertyChanged += (s, p) =>
        {
            RegisterCommand = new Command(RegisterAction, RegiterValidate);
        };
    }

    private async void RegisterAction(object obj)
    {
        try
        {
            IsBusy = true;
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
        finally { IsBusy = false; }
    }

    private bool RegiterValidate(object arg)
    {
        var result = validator.Validate(Model);
        if (result.Errors.Count > 0)
        {
            ErrorMessage = result.Errors.FirstOrDefault().ErrorMessage;
        }
        return result.IsValid;
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
        set { SetProperty(ref registerCommand, value); }
    }
    

    private void IsPasswordAction(object obj)
    {
        IsPassword = !IsPassword;
        PasswordIcon = IsPassword ? "\uf06e" : "\uf070";
    }

    private bool isPasword = true;

    public bool IsPassword
    {
        get { return isPasword; }
        set { SetProperty(ref isPasword, value); }
    }


    private string passwordIcon = "\uf070";

    public string PasswordIcon
    {
        get { return passwordIcon; }
        set { SetProperty(ref passwordIcon, value); }
    }

    public ICommand IsPasswordCommand { get; set; }



    private string errorMessage;

    public string ErrorMessage
    {
        get { return errorMessage; }
        set { SetProperty(ref errorMessage, value); }
    }



}

public class RegisterValidator : AbstractValidator<Pelapor>
{
    public RegisterValidator()
    {
        RuleFor((x) => x.Name).NotEmpty().WithMessage("nama Tidak Boleh Kosong!");
        RuleFor((x) => x.Email).NotEmpty().WithMessage("User Name Tidak Boleh Kosong")
            .EmailAddress().WithMessage("Masukkan Email Anda !");
        RuleFor((x) => x.PhoneNumber).NotEmpty().WithMessage("Nomor HP Tidak Boleh Kosong");
        RuleFor((x) => x.Address).NotEmpty().WithMessage("Alamat tidak boleh kosong !");
        RuleFor(request => request.Password)
             .NotEmpty()
             .MinimumLength(8).WithMessage("'{PropertyName}' Minimum 8 Karakter.")
             .Matches("[A-Z]").WithMessage("'{PropertyName}' Harus terdapat huruf kapital.")
             .Matches("[a-z]").WithMessage("'{PropertyName}' Harus terdapat huruf kecil.")
             .Matches(@"\d").WithMessage("'{PropertyName}' Harus terdapat angka.")
             .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' harus terdapat karakter spesial.")
             .Matches("^[^£# “”]*$").WithMessage("'{PropertyName}' tidak boleh tedapat £ # “” or spasi.");
        RuleFor((x) => x.ConfirmPassoword)
            .NotEmpty().WithMessage("Confirm Password Tidak Boleh Kosong! !")
            .Equal(x => x.Password).WithMessage("Password Harus Sama");
    }
}