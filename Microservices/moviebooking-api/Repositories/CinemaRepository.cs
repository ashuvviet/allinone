using AutoMapper;
using moviebooking_api.DTOs;
using moviebooking_api.Model;

namespace moviebooking_api.Repositories
{
    internal class CinemaRepository : ICinemaRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public CinemaRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddCinema(CinemaDto entity)
        {
            var city = await _context.GetCityAsync(entity.City);
            if(city == null)
            {
                city = _mapper.Map<City>(entity);
                await _context.CreateAsync(city);
            }

            city = await _context.GetCityAsync(entity.City);
            var cinema = _mapper.Map<Cinema>(entity);
            cinema.CityId = city.Id;

            await _context.CreateAsync(cinema);
        }

        public async Task<IEnumerable<Cinema>> GetAllCinema(string city)
        {
            return await _context.GetAllCinemaAsync(city);
        }

        public async Task AddMovie(MovieDto entity)
        {
            var movie = _mapper.Map<Movie>(entity);
            await _context.CreateAsync(movie);
        }

        public async Task AddCinemaMovie(MovieDto entity)
        {
            var city = await _context.GetCityAsync(entity.Cinema.City);
            var cinema = await _context.GetCinemaAsync(entity.Cinema.Name, city.Id);
            if(cinema.OwnerId != entity.Cinema.OwnerId)
            {
                throw new UnauthorizedAccessException("Cinema is not under same owner");
            }

            var masterMovie = await _context.GetMovieAsync(entity.Name);
            if (cinema != null && masterMovie != null)
            {
                var movie = _mapper.Map<CinemaMovie>(entity);                
                movie.CinemaId = cinema.Id;
                movie.MovieId = masterMovie.Id;
                movie.ShowTimings = entity.ShowTimings;
                await _context.CreateAsync(movie);
            }
            else
            {
                throw new InvalidOperationException("Master Data does not exist.");
            }
        }

        public async Task<IEnumerable<CinemaMovieDto>> GetCinemaByMovieAsync(string movieName, string city)
        {
            return await _context.GetCinemaByMovieAsync(movieName, city);
        }

        public async Task<IEnumerable<MovieDto>> GetCinemaMovieByCityAsync(string city)
        {
            return await _context.GetCinemaMovieByCityAsync(city);
        }
    }
}
