using BasarnasApp.Shared.Models;
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
        BindingContext = new ProfileViewModel();
    }
}

public class ProfileViewModel : BaseViewModel
{

    public Pelapor Model { get; set; } = new Pelapor();
    public ChangePassword Password { get; set; } = new ChangePassword();

    private ImageSource photo = ImageSource.FromFile("noimage.jpg");

    public ImageSource Photo
    {
        get { return photo; }
        set { SetProperty(ref photo, value); }
    }

    public ProfileViewModel()
    {
        ChangePasswordCommand = new Command(ChangePasswordAction, ChangePasswordValidation);
        TakePhotoCommand = new Command(async () => await TakePhoto());
        validator = new ProfileValidator();
        UpdateProfileCommand = new Command(ProfileAction, ProfileValidate);
        IsPasswordCommand = new Command(IsPasswordAction);
        Model = Account.GetProfile();
        if (!string.IsNullOrEmpty(Model.Photo))
        {
            Photo = ImageSource.FromUri(new Uri($"/images/profile/{Model.Photo}"));
        }
        Model.PropertyChanged += (s, p) =>
        {
            UpdateProfileCommand = new Command(ProfileAction, ProfileValidate);
        };

        Password.PropertyChanged += (s, p) =>
        {
            ChangePasswordCommand = new Command(ChangePasswordAction, ChangePasswordValidation);
        };
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
                    Model.PhotoData = result;
                    Photo = ImageSource.FromStream(() => new MemoryStream(Model.PhotoData));
                }
            }
        }
        catch (Exception ex)
        {
            await Helper.ShellMessageShow("Error", ex.Message);
        }
    }

    private async void ProfileAction(object obj)
    {
        try
        {
            IsBusy = true;
            var service = Helper.GetService<IPelaporService>();
            var result = await service.PutAsync(Model.Id, Model);
            if (result)
            {
                await Account.SetProfile(Model);
                await Application.Current.MainPage.DisplayAlert("Success", "Data Berhasil Disimpan !", "Ok");
            }
            else
            {
                throw new SystemException("User Tidak Berhasil Diubah !");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
        finally { IsBusy = false; }
    }

    private bool ProfileValidate(object arg)
    {
        return validator.Validate(Model).IsValid;
    }

    private void GotoLoginAction(object obj)
    {
        Application.Current.MainPage = Helper.GetService<LoginPage>();
    }

    public Command TakePhotoCommand { get; }

    private ProfileValidator validator;
    private ChangePasswordValidator changePasswordValidator = new ChangePasswordValidator();

    public ICommand GotoLoginCommand { get; set; }


    private ICommand updateProfileCommand;

    public ICommand UpdateProfileCommand
    {
        get { return updateProfileCommand; }
        set { SetProperty(ref updateProfileCommand, value); }
    }

    private bool ChangePasswordValidation(object arg)
    {
        PasswordMessageError = string.Empty;
        var validation = changePasswordValidator.Validate(Password);

        if (!validation.IsValid)
        {
            PasswordMessageError = validation.Errors.FirstOrDefault().ErrorMessage;
        }
        return validation.IsValid;
    }

    private async void ChangePasswordAction(object obj)
    {
        try
        {
            IsBusy = true;
            var service = Helper.GetService<IPelaporService>();
            Password.UserId = Model.UserId;
            Password.Email = Model.Email;
            bool isSuccess = await service.ChangePassword(Password);
            if (isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Berhasil !", "Ok");
            }
            else
                throw new SystemException("Tidak Berhasil !");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
        finally
        {
            IsBusy=false;
        }
    }

    private ICommand changePasswordCommand;

    public ICommand ChangePasswordCommand
    {
        get { return changePasswordCommand; }
        set { SetProperty(ref changePasswordCommand, value); }
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



    private string passwordMessageError;

    public string PasswordMessageError
    {
        get { return passwordMessageError; }
        set { SetProperty(ref passwordMessageError , value); }
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




public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.OldPassword)
      .NotEmpty().WithMessage("Old Password Required");
        RuleFor(x => x.NewPassword)
           .NotEmpty()
           .MinimumLength(8).WithMessage("Lenght of '{PropertyName}' Minimum 8.")
           .Matches("[A-Z]").WithMessage("'{PropertyName}' must contain one or more capital letters.")
           .Matches("[a-z]").WithMessage("'{PropertyName}' must contain one or more lowercase letters.")
           .Matches(@"\d").WithMessage("'{PropertyName}' must contain one or more digits.")
           .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'{PropertyName}' must contain one or more special characters.")
           .Matches("^[^£# “”]*$").WithMessage("'{PropertyName}' must not contain the following characters £ # “” or spaces.");

        RuleFor(x => x.ConfirmPassword)
        .NotEmpty().WithMessage("Confirm Password Required").When(x => !string.IsNullOrEmpty(x.NewPassword))
        .Equal(x => x.NewPassword).WithMessage("Password Not Same !");
    }
}





public class ChangePassword : BaseNotify
{

    public string? UserId { get; set; }
    public string? Email { get; set; }


    private string oldPassword;

    public string OldPassword
    {
        get { return oldPassword; }
        set { SetProperty(ref oldPassword, value); }
    }


    private string newPassword;

    public string NewPassword
    {
        get { return newPassword; }
        set { SetProperty(ref newPassword, value); }
    }


    private string confirmPassword;

    public string ConfirmPassword
    {
        get { return confirmPassword; }
        set { SetProperty(ref confirmPassword, value); }
    }



}