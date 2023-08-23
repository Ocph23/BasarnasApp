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
        IsPasswordCommand = new Command(IsPasswordAction);

        

        this.PropertyChanged += (s, p) =>
        {
            if (p.PropertyName != "LoginCommand")
            {
                LoginCommand = new Command(LoginAction, LoginValidation);
            }
        };

    }

    private void IsPasswordAction(object obj)
    {
        IsPassword = !IsPassword;
        PasswordIcon = IsPassword ? "\uf06e" : "\uf070";
    }

    private async void LoginAction(object obj)
    {
        try
        {
            IsBusy = true;
            var accountService = Helper.GetService<IAccountService>();
            var login =await accountService.Login(new Models.LoginRequest (UserName,Password));
            if(login != null)
            {
               await Account.SetUser(login);
                Application.Current.MainPage = new AppShell(); 
            }
            else
            {
                throw new SystemException("Anda tidak memiliki akses.");
            }
        }
        catch (Exception ex)
        {
          await  Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
        finally
        {
            IsBusy =false;
        }
    }

    private bool LoginValidation(object arg)
    {
        if(IsBusy) 
            return false;
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



    private bool isPasword=true;

    public bool IsPassword
    {
        get { return isPasword; }
        set { SetProperty(ref isPasword , value); }
    }


    private string passwordIcon= "\uf070";

    public string PasswordIcon
    {
        get { return passwordIcon; }
        set { SetProperty(ref passwordIcon , value); }
    }

    public ICommand IsPasswordCommand { get; set; }

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