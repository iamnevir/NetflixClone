using NetflixClone.ViewModels;

namespace NetflixClone.Pages;

public partial class DetailMovie : ContentPage
{
	readonly DetailViewModel _viewModel;
	public DetailMovie(DetailViewModel detailViewModel)
	{
		InitializeComponent();
		_viewModel = detailViewModel;
		BindingContext = _viewModel;
	}
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > 0)
        {
            _viewModel.SimilarItemWidth= Convert.ToInt32(width/3)-3;
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.InitializeAsync();
    }

    private void TrailerTab_Tapped(object sender, TappedEventArgs e)
    {
        similarTabIni.Color = Colors.Black;
        similarTabContent.IsVisible = false;
        trailerTabIni.Color = Colors.Red;
        trailerTabContent.IsVisible = true;
    }

    private void SimilarTab_Tapped(object sender, TappedEventArgs e)
    {
        similarTabIni.Color = Colors.Red;
        similarTabContent.IsVisible = true;
        trailerTabIni.Color = Colors.Black;
        trailerTabContent.IsVisible = false;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await pageScrollView.ScrollToAsync(0, 0, animated: true);
    }
}