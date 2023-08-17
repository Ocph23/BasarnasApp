namespace BasarnasMobilaApp.Pages;

public partial class PhotoViewPage : ContentPage
{
	public PhotoViewPage(string photo)
	{
		InitializeComponent();
		Photo = ImageSource.FromUri(new Uri($"{Helper.Url}{photo}"));
		BindingContext = this;
	}

	private ImageSource _photo;

	public ImageSource Photo
	{
		get { return _photo; }
		set { _photo = value;
			OnPropertyChanged(nameof(Photo));
		}
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Shell.Current.Navigation.PopModalAsync();
    }
}