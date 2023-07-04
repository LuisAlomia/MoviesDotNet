using AutoMapper;
using Domain.DTOs.Responses;
using Domain.Entites;
using Domain.Services;
using Infrastructure.ResponseApi;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieServices movieServices;
        private readonly IMapper mapper;

        private ResponseApi responseApi;

        public MovieController(IMovieServices movieServices, IMapper mapper)
        {
            this.movieServices = movieServices;
            this.mapper = mapper;
            responseApi = new ResponseApi();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseApi>> GetAllMovies()
        {
            try
            {
                IEnumerable<Movie> listMovies = await this.movieServices.GetAllMovies();

                responseApi.result = this.mapper.Map<IEnumerable<MovieResponseDTO>>(listMovies);
                responseApi.statusCode = HttpStatusCode.OK;
                responseApi.isSuccessful = true;

                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.isSuccessful = false;
                responseApi.errorMessage = new List<string>() { ex.ToString() };
                responseApi.statusCode = HttpStatusCode.InternalServerError;
            }

            return Ok(responseApi);
        }

        [HttpGet("movie/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseApi>> GetMovie(int id)
        {
            try
            {
                Movie movie = await this.movieServices.GetMovie(id);

                if (movie is null)
                {
                    responseApi.isSuccessful = false;
                    responseApi.errorMessage = new List<string>() { $"Id {id} not found" };
                    responseApi.statusCode = HttpStatusCode.NotFound;

                    return NotFound(responseApi);
                }

                responseApi.result = this.mapper.Map<MovieResponseDTO>(movie);
                responseApi.statusCode = HttpStatusCode.OK;
                responseApi.isSuccessful = true;

                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.isSuccessful = false;
                responseApi.errorMessage = new List<string>() { ex.ToString() };
                responseApi.statusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(responseApi);
        }
    }
}
