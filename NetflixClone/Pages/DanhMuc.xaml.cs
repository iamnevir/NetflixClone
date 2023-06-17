
using NetflixClone.ViewModels;

namespace NetflixClone.Pages;

public partial class DanhMuc : ContentPage
{
    CategoryViewModel _categoryViewModel;
    public DanhMuc(CategoryViewModel categoryViewModel)
	{
		InitializeComponent();
        _categoryViewModel = categoryViewModel;
        BindingContext = _categoryViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _categoryViewModel.InitializeAsync();
    }

    private async void BtnCloseDanhMuc_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}