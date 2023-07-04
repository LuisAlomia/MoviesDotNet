using AutoMapper;
using Domain.DTOs.Responses;
using Domain.Entites;
using Domain.Services;
using Infrastructure.ResponseApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        private ResponseApi responseApi;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            this.userServices = userServices;
            this.mapper = mapper;
            responseApi = new ResponseApi();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseApi>> GetAll()
        {
            try
            {
                IEnumerable<User> listUsers = await userServices.GetAllUser();

                responseApi.result = this.mapper.Map<IEnumerable<UserResponseDTO>>(listUsers);
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

        [Authorize(Policy="RoleAdmin")]
        [HttpGet("name/(name)")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ResponseApi>> GetAllByName(string name)
        {
            try
            {
                IEnumerable<User> listUsers = await userServices.GetAllUserByName(name);

                responseApi.result = this.mapper.Map<IEnumerable<UserResponseDTO>>(listUsers);
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseApi>> GetUser(int id)
        {
            try
            {
                User user = await this.userServices.GetUser(id);

                if (user is null)
                {
                    responseApi.isSuccessful = false;
                    responseApi.errorMessage = new List<string>() { $"Id {id} not found" };
                    responseApi.statusCode = HttpStatusCode.NotFound;

                    return NotFound(responseApi);
                }

                responseApi.result = this.mapper.Map<UserResponseDTO>(user);
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
