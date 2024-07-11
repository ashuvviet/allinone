using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using moviebooking_api.DTOs;
using moviebooking_api.Options;
using System.Xml.Linq;

namespace moviebooking_api.Model
{
    internal class DbContext
    {
        private readonly IMongoCollection<City> _cityCollection;
        private readonly IMongoCollection<Cinema> _cinemaCollection;
        private readonly IMongoCollection<Movie> _movieCollection;
        private readonly IMongoCollection<CinemaMovie> _cinemaMovieCollection;

        private readonly ILogger<DbContext> _logger;

        public DbContext(IOptions<MongoDbOptions> config, ILogger<DbContext> logger)
        {
            _logger = logger;
            var mongoDbConfig = config.Value;
            try
            {
                _logger.LogDebug($"Mongo String {mongoDbConfig.ConnectionString}");
                var databaseName = MongoUrl.Create(mongoDbConfig.ConnectionString)?.DatabaseName;
                var dbClient = new MongoClient(mongoDbConfig.ConnectionString.Replace("{UserName}",
                    mongoDbConfig.UserName).Replace("{Password}", mongoDbConfig.Password));
                var db = dbClient.GetDatabase(databaseName);

                _cityCollection = db.GetCollection<City>(mongoDbConfig.CityCollectionName);
                _cinemaCollection = db.GetCollection<Cinema>(mongoDbConfig.CinemaCollectionName);
                _movieCollection = db.GetCollection<Movie>(mongoDbConfig.MovieCollectionName);
                _cinemaMovieCollection = db.GetCollection<CinemaMovie>("Cinema" + mongoDbConfig.MovieCollectionName);

                _logger.LogDebug("created mongo collection");
            }
            catch (MongoConfigurationException)
            {
                _logger.LogError("Invalid mongo configuration or ConnectionString!");
                throw new Exception("Invalid mongo configuration or ConnectionString!");
            }
        }

        public async Task<City> GetCityAsync(string name) => await _cityCollection.Find(x => x.Name == name).FirstOrDefaultAsync();


        public async Task CreateAsync(City city)
        {
            var user = await GetCityAsync(city.Name);
            if (user == null)
            {
                city.Id = await _cityCollection.CountDocumentsAsync(s => true) + 1;
                await _cityCollection.InsertOneAsync(city);
            }
        }

        public async Task<Cinema> GetCinemaAsync(string name) => await _cinemaCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<Cinema> GetCinemaAsync(string name, long cityId) => await _cinemaCollection.Find(x => x.Name == name && x.CityId == cityId).FirstOrDefaultAsync();

        public async Task<IEnumerable<Cinema>> GetAllCinemaAsync(string city)
        {
            var cityObj = await GetCityAsync(city);
            if (cityObj == null) return default;
            return await _cinemaCollection.Find(s => s.CityId == cityObj.Id).ToListAsync();
        }

        public async Task CreateAsync(Cinema cinema)
        {
            var cinemaObj = await GetCinemaAsync(cinema.Name, cinema.CityId);
            if (cinemaObj == null)
            {
                cinema.Id = await _cinemaCollection.CountDocumentsAsync(s => true) + 1;
                await _cinemaCollection.InsertOneAsync(cinema);
            }
        }

        public async Task<Movie> GetMovieAsync(string name) => await _movieCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<IEnumerable<Movie>> GetAllMovieAsync() => await _movieCollection.Find(s => true).ToListAsync();

        public async Task CreateAsync(Movie masterMovie)
        {
            var mObj = await GetMovieAsync(masterMovie.Name);
            if (mObj == null)
            {
                masterMovie.Id = await _movieCollection.CountDocumentsAsync(s => true) + 1;
                await _movieCollection.InsertOneAsync(masterMovie);
            }
        }

        public async Task<IEnumerable<MovieDto>> GetCinemaMovieByCityAsync(string city)
        {
            var list = new List<MovieDto>();
            var masterMovieData = await GetAllMovieAsync();
            var cinemas = await GetAllCinemaAsync(city);
            foreach (var cinema in cinemas)
            {
                var movies = await GetCinemaMovieAsync(cinema.Name, cinema.CityId);
                foreach (var movie in movies)
                {
                    list.Add(new MovieDto() 
                    { 
                        Name = masterMovieData.First(s => s.Id == movie.MovieId).Name, 
                        ShowTimings = movie.ShowTimings,
                        Cinema = new CinemaDto() 
                        { 
                            Name = cinema.Name, 
                            City = city, 
                            OwnerId = cinema.OwnerId 
                        } 
                    });
                }
            }

            return list;
        }

        public async Task<IEnumerable<CinemaMovieDto>> GetCinemaByMovieAsync(string movieName, string city)
        {
            var list = new List<CinemaMovieDto>();
            var masterMovieData = await GetMovieAsync(movieName);
            var cinemas = await GetAllCinemaAsync(city);
            foreach (var cinema in cinemas)
            {
                var movies = await GetCinemaMovieAsync(cinema.Name, cinema.CityId);
                foreach (var movie in movies.Where(s => s.MovieId == masterMovieData.Id))
                {
                    list.Add(new CinemaMovieDto()
                    {
                        Name = cinema.Name,
                        City = city,
                        ShowTimings = movie.ShowTimings
                    });
                }
            }

            return list;
        }

        public async Task<IEnumerable<CinemaMovie>> GetCinemaMovieAsync(string cinemaName, long cityId)
        {
            var cinema = await GetCinemaAsync(cinemaName, cityId);  
            return _cinemaMovieCollection.Find(s => s.CinemaId == cinema.Id).ToList();
        }

        public async Task CreateAsync(CinemaMovie movie)
        {
            if(_cinemaMovieCollection.Find(s => s.CinemaId == movie.CinemaId && s.MovieId == movie.MovieId).Any())
            {
                return;
            }

            movie.Id = await _cinemaMovieCollection.CountDocumentsAsync(s => true) + 1;
            await _cinemaMovieCollection.InsertOneAsync(movie);
        }
    }
}
