using NetflixClone.ViewModels;

namespace NetflixClone.Pages;

public partial class NewHotPage : ContentPage
{
    readonly NewHotViewModel _newHotViewModel;
    public NewHotPage(NewHotViewModel newHotViewModel)
	{
		InitializeComponent();
        _newHotViewModel = newHotViewModel;
        BindingContext= _newHotViewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _newHotViewModel.InitializeAsync();
    }
    private void MoiNguoiDangXem_Tab(object sender, TappedEventArgs e)
    {
        MoiNguoiDangXemTabIni.BackgroundColor = Colors.White;
        MoiNguoiDangXem.TextColor = Colors.Black;
        moivahotTabIni.BackgroundColor = Colors.Black;
        MoiVaHot.TextColor = Colors.White;
        sapramatTabContent.IsVisible = false;
        moinguoiTabContent.IsVisible = true;
    }

    private void MoiVaHot_Tab(object sender, TappedEventArgs e)
    {
        MoiNguoiDangXemTabIni.BackgroundColor = Colors.Black;
        MoiNguoiDangXem.TextColor = Colors.White;
        moivahotTabIni.BackgroundColor = Colors.White;
        MoiVaHot.TextColor = Colors.Black;
        moinguoiTabContent.IsVisible = false;
        sapramatTabContent.IsVisible = true;
    }
}