using Domain.Entites;

namespace Domain.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovie(int id);
        Task<bool> CreateMovie(Movie movie);
        Task<bool> UpdateMovie(int id, Movie movie);
        Task<bool> DeleteMovie(int id);
    }
}
