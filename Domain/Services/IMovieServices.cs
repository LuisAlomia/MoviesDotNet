using Domain.Entites;

namespace Domain.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie?> GetMovie(int id);
        Task<Movie> CreateMovie(Movie movie);
        Task<bool> UpdateMovie(int id, Movie movie);
        Task<bool> DeleteMovie(int id);
    }
}
