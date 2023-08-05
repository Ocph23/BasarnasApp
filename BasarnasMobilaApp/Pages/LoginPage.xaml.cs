using BasarnasMobilaApp.Services;
using FluentValidation;
using System.Windows.Input;

namespace BasarnasMobilaApp.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}

public class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    {
        GotoRegisterCommand = new Command(GotoRegisterAction);
        LoginCommand = new Command(LoginAction, LoginValidation);

        this.PropertyChanged += (s, p) =>
        {
            if (p.PropertyName != "LoginCommand")
            {
                LoginCommand = new Command(LoginAction, LoginValidation);
            }
        };

    }

    private async void LoginAction(object obj)
    {
        try
        {
            var accountService = Helper.GetService<IAccountService>();
            var login =await accountService.Login(new Models.LoginRequest (UserName,Password));
            if(login != null)
            {
               await Account.SetUser(login);
            }
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }

    private bool LoginValidation(object arg)
    {
        var validator = new LoginValidator();
        var validatorResult = validator.Validate(this);
        if (validatorResult.IsValid)
        {
            return true;
        }
        return false;
    }

    private void GotoRegisterAction(object obj)
    {
        Application.Current.MainPage = Helper.GetService<RegisterPage>();
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


    public ICommand GotoRegisterCommand { get; set; }

    private ICommand loginCommand;

    public ICommand LoginCommand
    {
        get { return loginCommand; }
        set { SetProperty(ref loginCommand, value); }
    }


}



public class LoginValidator : AbstractValidator<LoginViewModel>
{
    public LoginValidator()
    {
        RuleFor((x) => x.UserName).EmailAddress().WithMessage("Masukkan Email Anda !");
        RuleFor((x) => x.Password).NotEmpty().WithMessage("Password Tidak Boleh Kosong");

    }
}