

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetflixClone.Models;
using NetflixClone.Pages;
using NetflixClone.Services;
using System.Collections.ObjectModel;

namespace NetflixClone.ViewModels;

[QueryProperty(nameof(Media),nameof(Media))]
public partial class DetailViewModel: ObservableObject
{
    readonly TmdbServices _tmdbServices;
    public DetailViewModel(TmdbServices tmdbServices)
    {
        _tmdbServices = tmdbServices;
    }
    [ObservableProperty]
    Media _media;
    [ObservableProperty]
    string _mainTrailerUrl;
    [ObservableProperty]
    bool _isBusy;
    [ObservableProperty]
    int _runtime;
    [ObservableProperty]
    int _similarItemWidth = 125; 

    public ObservableCollection<Video> Videos { get; set; } = new();
    public ObservableCollection<Media> Similars { get; set; } = new();
    public async Task InitializeAsync()
    {
        var similarMediasTask = _tmdbServices.GetSimilarAsync(Media.Id, Media.MediaType);
        IsBusy = true;
        try
        {
            var trailerTeaserTask =  _tmdbServices.GetTrailersAsync(Media.Id, Media.MediaType);
            var detailsTask =  _tmdbServices.GetMediaDetailsAsync(Media.Id, Media.MediaType);

            var trailerTeaser = await trailerTeaserTask;
            var details=await detailsTask;
            if (trailerTeaser?.Any() == true)
            {
                var trailer = trailerTeaser.FirstOrDefault(t => t.type == "Trailer");
                trailer ??= trailerTeaser.First();
                MainTrailerUrl = GenerateYtbUrl(trailer.key);
                
               foreach(var video in trailerTeaser)
                {
                    Videos.Add(video);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Không thấy", "Không tìm thấy trailer", "Ok");
            }
            if (details is not null)
            {
                Runtime = details.runtime;
            }
            var similarMedias = await similarMediasTask;
            if (similarMedias is not null)
            {
                foreach(var media in similarMedias)
                {
                    Similars.Add(media);
                }
                
            }
        }
        finally { IsBusy = false; }
    }
    [RelayCommand]
    async Task ChangeToThisMedia(Media media)
    {
        var para = new Dictionary<string, object>
        {
            [nameof(Media)] = Media
        };
        await Shell.Current.GoToAsync(nameof(DetailMovie), true, para);
    }
    [RelayCommand]
    void SetMainTrailer(string vdkey)=> MainTrailerUrl = GenerateYtbUrl(vdkey);
    static string GenerateYtbUrl(string key) => $"https://www.youtube.com/embed/{key}";
}