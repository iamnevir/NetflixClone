﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using NetflixClone.Pages;
using NetflixClone.Services;
using NetflixClone.ViewModels;

namespace NetflixClone;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
            });


        builder.Services.AddHttpClient(TmdbServices.TmdbHttpClientName, httpClient =>
        httpClient.BaseAddress = new Uri("https://api.themoviedb.org"));

        builder.Services.AddSingleton<TmdbServices>();

        builder.Services.AddSingleton<CategoryViewModel>();
        builder.Services.AddSingleton<DanhMuc>();

        builder.Services.AddSingleton<HomeViewModel>();
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddSingleton<NewHotViewModel>();
        builder.Services.AddSingleton<NewHotPage>();

        builder.Services.AddSingleton<Download>();

        builder.Services.AddTransientWithShellRoute<DetailMovie,DetailViewModel>(nameof(DetailMovie));
        return builder.Build();
    }
}
