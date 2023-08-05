namespace BasarnasMobilaApp.Pages;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();
        LogoutCommand = new Command(() => {
			Application.Current.MainPage = Helper.GetService<LoginPage>();
		});
		BindingContext = this;
	}

    public Command LogoutCommand { get; }
}