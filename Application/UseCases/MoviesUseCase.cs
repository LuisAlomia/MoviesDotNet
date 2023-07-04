using Domain.Entites;
using Domain.Repositories;
using Domain.Services;

namespace Application.UseCases
{
    public class MoviesUseCase : IMovieServices
    {
        private readonly IMovieRepository repository;

        public MoviesUseCase(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await repository.GetAllMovies();
        }

        public async Task<Movie?> GetMovie(int id)
        {
            Movie movie = await repository.GetMovie(id);

            if (movie == null) return null; 

            return movie;
        }

        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMovie(int id, Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
