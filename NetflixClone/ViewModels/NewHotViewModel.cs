using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using NetflixClone.Models;
using NetflixClone.Services;
using System.Collections.ObjectModel;

namespace NetflixClone.ViewModels;


public partial class NewHotViewModel : ObservableObject
{
    readonly TmdbServices _tmdbServices;
    public NewHotViewModel(TmdbServices tmdbServices)
    {
        _tmdbServices = tmdbServices;
    }
    [ObservableProperty]
    bool _isBusy;
    [ObservableProperty]
    string _mainTrailerUrl;
    [ObservableProperty]
    Media _media;
    public ObservableCollection<Video> Videos { get; set; } = new();
    public ObservableCollection<string> Trailers { get; set; } = new();
    public async Task InitializeAsync()
    {
        IsBusy = true;
        var upComingListTask = _tmdbServices.GetUpComingAsync();
        var sapChieuListTask = _tmdbServices.GetSapChieuAsync();
        var genresListTask = _tmdbServices.GetGenresAsync();
        try
        {
            var upComingList = await upComingListTask;
            var sapChieuList = await sapChieuListTask;
            var genresList = await genresListTask;
            if (upComingList?.Any() == true)
            {
                foreach (var video in upComingList)
                {

                    var upComingMedia = _tmdbServices.GetTrailersAsync(video.Id, video.MediaType);
                    var videodetailTask = _tmdbServices.GetMediaDetailsAsync(video.Id, video.MediaType);
                    var upComing = await upComingMedia;
                    var videodetail = await videodetailTask;
                    if (upComing?.Any() == true)
                    {
                        var mda = upComing.FirstOrDefault(t => t.type == "Trailer");
                        mda ??= upComing.First();
                        video.TrailerUrl = DetailViewModel.GenerateYtbUrl(mda.key);
                    }
                    var a = new List<string>();
                    foreach (var v in video.Genres)
                    {
                        foreach (var gen in genresList)
                        {
                            if (v == gen.Id)
                            {
                                a.Add(gen.Name.Replace("Phim", ""));
                                
                            }
                        }
                    }
                    DateTime date = DateTime.Parse(video.ReleaseDate);
                    video.Date = date.Month.ToString();
                    video.Time = date.Day.ToString();
                    video.Genres_Name = a.ToArray();
                    Media = video;
                }


            }
            else
            {
                await Shell.Current.DisplayAlert("Không thấy", "Không tìm thấy phim sắp ra mắt", "Ok");
            }


        }
        finally
        {
            IsBusy = false;
        }

    }
}
