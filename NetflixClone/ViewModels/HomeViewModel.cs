

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using NetflixClone.Models;
using NetflixClone.Services;
using System.Collections.ObjectModel;

namespace NetflixClone.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    readonly TmdbServices _tmdbServices;
    public HomeViewModel(TmdbServices tmdbServices)
    {
        _tmdbServices = tmdbServices;
    }
    [ObservableProperty]
    Media _trendingMovie;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowMovieDetail))]
    Media? _selectedMedia;
   
    public bool ShowMovieDetail =>SelectedMedia != null;
    public ObservableCollection<Media> Trending { get; set; } = new();
    public ObservableCollection<Media> TopRated { get; set; } = new();
    public ObservableCollection<Media> NetflixOriginals { get; set; } = new();
    public ObservableCollection<Media> ActionMovies { get; set; } = new();
    public ObservableCollection<Media> UpComing { get; set; } = new();
    public ObservableCollection<Media> AiringToday { get; set; } = new();
    public ObservableCollection<Media> TvPopular { get; set; } = new();
    public ObservableCollection<Media> TvTopRated { get; set; } = new();
    public ObservableCollection<Media> NowPlaying { get; set; } = new();
    public ObservableCollection<Media> SapChieu { get; set; } = new();
    public async Task InitializeAsync()
    {
        var trendingListTask = _tmdbServices.GetTrendingAsync();
        var topRatedListTask = _tmdbServices.GetTopRatedAsync();
        var netflixOriginalsListTask = _tmdbServices.GetNetflixOriginalAsync();
        var actionMoviesListTask = _tmdbServices.GetActionAsync();
        var upComingListTask=_tmdbServices.GetUpComingAsync();
        var airingTodayListTask =_tmdbServices.GetAiringTodayAsync();
        var tvPopularListTask =_tmdbServices.GetTvPopularAsync();
        var tvTopRatedListTask = _tmdbServices.GetTvTopRatedAsync();
        var nowPlayingListTask = _tmdbServices.GetNowPlayingAsync();
        var sapChieuListTask =_tmdbServices.GetSapChieuAsync();
        var medias = await Task.WhenAll(
            trendingListTask,
            topRatedListTask,
            netflixOriginalsListTask,
            actionMoviesListTask,
            upComingListTask,
            airingTodayListTask,
            tvPopularListTask,
            tvTopRatedListTask,
            nowPlayingListTask,
            sapChieuListTask);

        var trendingList = medias[0];
        var topRatedList = medias[1];
        var netflixOriginalsList = medias[2];
        var actionMoviesList = medias[3];
        var upComingList = medias[4];
        var airingTodayList = medias[5];
        var tvPopularList = medias[6];    
        var tvTopRatedList = medias[7];
        var nowPlayingList = medias[8];
        var sapChieuList = medias[9];

        TrendingMovie = trendingList.OrderBy(t => Guid.NewGuid())
            .FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.DisplayTitle)
            && !string.IsNullOrWhiteSpace(t.Thumbnail));

        SetMediaCollection(trendingList, Trending);
        SetMediaCollection(topRatedList, TopRated);
        SetMediaCollection(netflixOriginalsList, NetflixOriginals);
        SetMediaCollection(actionMoviesList, ActionMovies);
        SetMediaCollection(upComingList, UpComing);
        SetMediaCollection(airingTodayList, AiringToday);
        SetMediaCollection(tvPopularList, TvPopular);
        SetMediaCollection(tvTopRatedList, TvTopRated);
        SetMediaCollection(nowPlayingList, NowPlaying);
        SetMediaCollection(sapChieuList, SapChieu);

    }
    void SetMediaCollection(IEnumerable<Media> medias, ObservableCollection<Media> collection)
    {
        collection.Clear();
        foreach (var media in medias)
        {
            collection.Add(media);
        }
    }
    [RelayCommand]
    void SelectMedia(Media? media = null)
    {
        if(media is not null)
        {
            if(media.Id == SelectedMedia?.Id)
            {
                media= null;
            }
        }
        SelectedMedia = media;
    }
}
