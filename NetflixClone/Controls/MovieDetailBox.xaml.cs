using NetflixClone.Models;
using NetflixClone.Pages;
using NetflixClone.ViewModels;
using System.Windows.Input;

namespace NetflixClone.Controls;

public partial class MovieDetailBox : ContentView
{
    public static readonly BindableProperty MediaProperty =
        BindableProperty.Create(nameof(Media), typeof(Media), typeof(MovieDetailBox), null);
    public event EventHandler Closed;
    public MovieDetailBox()
	{
		InitializeComponent();
        ClosedCommand = new Command(ExecuteMediaDetailsCommand);
	}
    public Media Media
    {
        get => (Media)GetValue(MediaProperty); set => SetValue(MediaProperty, value);
    }
    public ICommand ClosedCommand { get; private set; }
    void ExecuteMediaDetailsCommand(object parameter)
    {
        if (parameter is Media media && media is not null)
        {
            Closed?.Invoke(this, new MediaSelectEventArgs(media));
        }
    }

    private void Button_Clicked(object sender, EventArgs e) => Closed?.Invoke(this,EventArgs.Empty);

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var para = new Dictionary<string, object>
        {
            [nameof(DetailViewModel.Media)] = Media
        };
        await Shell.Current.GoToAsync(nameof(DetailMovie),true,para);
    }
}