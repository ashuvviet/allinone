using Microsoft.AspNetCore.Identity;
using moviebooking_api.Repositories;

namespace moviebooking_api
{
    public class SeedData
    {
        public async static Task EnsureSeedData(WebApplication app)
        {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var cinemaRepository = scope.ServiceProvider.GetRequiredService<ICinemaRepository>();
            var cinames = await cinemaRepository.GetAllCinema("new york");
            if (cinames == null)
            {
                var c1 = new DTOs.CinemaDto() { City = "new york", Name = "quad cinema", OwnerId = "201" };
                var c2 = new DTOs.CinemaDto() { City = "new york", Name = "amc", OwnerId = "202" };
                var c3 = new DTOs.CinemaDto() { City = "new york", Name = "landmark", OwnerId = "203" };
                var c4 = new DTOs.CinemaDto() { City = "cebu", Name = "ayala", OwnerId = "251" };
                var c5 = new DTOs.CinemaDto() { City = "cebu", Name = "sm", OwnerId = "252" };

                await cinemaRepository.AddCinema(c1);
                await cinemaRepository.AddCinema(c2);
                await cinemaRepository.AddCinema(c3);
                await cinemaRepository.AddCinema(c4);
                await cinemaRepository.AddCinema(c5);

                await cinemaRepository.AddMovie(new DTOs.MovieDto() { Name = "titanic" });
                await cinemaRepository.AddMovie(new DTOs.MovieDto() { Name = "saw x" });
                await cinemaRepository.AddMovie(new DTOs.MovieDto() { Name = "barbie" });
                await cinemaRepository.AddMovie(new DTOs.MovieDto() { Name = "harry potter" });

                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "barbie", Cinema = c1, ShowTimings = new List<string> { "1PM", "3PM", "6PM", "9PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "titanic", Cinema = c1, ShowTimings = new List<string> { "2PM", "4PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "harry potter", Cinema = c1, ShowTimings = new List<string> { "12PM", "9PM" } });

                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "titanic", Cinema = c2, ShowTimings = new List<string> { "12PM", "3:30PM", "6:30PM", "9PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "barbie", Cinema = c2, ShowTimings = new List<string> { "1PM", "3PM" } });

                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "barbie", Cinema = c3, ShowTimings = new List<string> { "12PM", "3:30PM", "6:30PM", "9PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "saw x", Cinema = c3, ShowTimings = new List<string> { "1PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "harry potter", Cinema = c3, ShowTimings = new List<string> { "4PM" } });

                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "barbie", Cinema = c4, ShowTimings = new List<string> { "1PM", "3PM", "6PM", "9PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "saw x", Cinema = c4, ShowTimings = new List<string> { "2PM", "4PM" } });

                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "harry potter", Cinema = c5, ShowTimings = new List<string> { "1PM", "3PM", "6PM", "9PM" } });
                await cinemaRepository.AddCinemaMovie(new DTOs.MovieDto() { Name = "barbie", Cinema = c5, ShowTimings = new List<string> { "2PM", "4PM" } });

            }
        }
    }
}
