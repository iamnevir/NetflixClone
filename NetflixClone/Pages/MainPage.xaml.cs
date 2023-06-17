
using NetflixClone.ViewModels;

namespace NetflixClone.Pages;

public partial class MainPage : ContentPage
{
	readonly HomeViewModel _homeViewModel;

	public MainPage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		_homeViewModel = homeViewModel;
		BindingContext = _homeViewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _homeViewModel.InitializeAsync();
    }

    private void MovieRow_MediaSelected(object sender, Controls.MediaSelectEventArgs e)
    {
        _homeViewModel.SelectMediaCommand.Execute(e.Media);
    }

    private void MovieDetailBox_Closed(object sender, EventArgs e)
    {
        _homeViewModel.SelectMediaCommand.Execute(null);
    }

    private async void DanhMucPhim_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DanhMuc));
    }
}

