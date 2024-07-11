using moviebooking_api.DTOs;
using moviebooking_api.Model;

namespace moviebooking_api.Repositories
{
    public interface ICinemaRepository
    {
        Task AddCinema(CinemaDto entity);
        Task<IEnumerable<Cinema>> GetAllCinema(string city);
        Task AddMovie(MovieDto entity);
        Task AddCinemaMovie(MovieDto entity);
        Task<IEnumerable<CinemaMovieDto>> GetCinemaByMovieAsync(string movieName, string city);
        Task<IEnumerable<MovieDto>> GetCinemaMovieByCityAsync(string city);
    }
}
