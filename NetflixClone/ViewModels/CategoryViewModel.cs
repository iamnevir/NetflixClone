
using CommunityToolkit.Mvvm.ComponentModel;
using NetflixClone.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NetflixClone.ViewModels;

public partial class CategoryViewModel : ObservableObject
{
    TmdbServices _services;
    IEnumerable<Genre> _genresList;
    public ObservableCollection<string> Categories { get; set; }
    public CategoryViewModel(TmdbServices tmdbServices)
    {
        _services = tmdbServices;

        Categories = new ObservableCollection<string>(new string[]
        {"Trang chủ" ,
            "Danh sách của tôi",
            "Có thể tải xuống" });
    }

    public async Task InitializeAsync()
    {
        _genresList ??= await _services.GetGenresAsync();
        Categories.Clear();
        Categories.Add("Trang chủ");
        Categories.Add("Danh sách của tôi");
        Categories.Add("Có thể tải xuống");
        foreach (var category in await _services.GetGenresAsync())
        {
            Categories.Add(category.Name);
        }
    }
}
