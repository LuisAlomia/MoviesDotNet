using Domain.Entites;
using Domain.Repositories;
using Infrastructure.Persistences.EntityFramework.ContextDB;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.EntityFramework.Implementations
{
    public class MovieRepoImpl : IMovieRepository
    {
        private readonly ContextDatabase ContextDB; 
        public MovieRepoImpl(ContextDatabase ContextDB) 
        {
            this.ContextDB = ContextDB;
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            IEnumerable<Movie> listMovies = await this.ContextDB.Movies.ToListAsync();

            return listMovies;
        }

        public async Task<Movie> GetMovie(int id)
        {
            Movie movie = await ContextDB.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

            if (movie == null) return null;

            return movie;
        }

        public async Task<bool> CreateMovie(Movie movie)
        {
            await ContextDB.Movies.AddAsync(movie);

            await ContextDB.SaveChangesAsync();

            return true;
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
