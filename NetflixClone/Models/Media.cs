

using NetflixClone.Services;

namespace NetflixClone.Models;

public class Media
{
    public int Id { get; set; }
    public string DisplayTitle { get; set; }
    public string MediaType { get; set; } // "movie" or "tv"
    public int[] Genres { get; set; }

    public string Thumbnail { get; set; }
    public string ThumbnailSmall { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Overview { get; set; }
    public string ReleaseDate { get; set; }
    public string TrailerUrl { get; set; }
    public string[] Genres_Name { get; set; }

}
