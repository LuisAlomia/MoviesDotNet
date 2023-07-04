using Domain.DTOs.Request;
using Domain.Entites;
using Domain.Services;
using Infrastructure.ResponseApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUserServices authServices;
        private IConfiguration configuration;
        private ResponseApi responseApi;        

        public AuthController(IAuthUserServices authUserServices, IConfiguration configuration)
        {
            this.authServices = authUserServices;
            this.configuration = configuration;
            responseApi = new ResponseApi();
            
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseApi>> Register([FromBody] User user)
        {
            try
            {
                User newUser = await this.authServices.Register(user);

                if (newUser is null)
                {
                    responseApi.isSuccessful = false;
                    responseApi.errorMessage = new List<string>() { $"email {user.Email} exist" };
                    responseApi.statusCode = HttpStatusCode.BadRequest;

                    return BadRequest(responseApi);
                }

                responseApi.result = newUser;
                responseApi.statusCode = HttpStatusCode.OK;
                responseApi.isSuccessful = true;

                return Ok(responseApi); //(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.isSuccessful = false;
                responseApi.errorMessage = new List<string>() { ex.ToString() };
                responseApi.statusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(responseApi);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResponseApi>> Login([FromBody] AuthUser credentials)
        {
            try
            {
                User user = await this.authServices.Login(credentials);

                if (user is null)
                {
                    responseApi.isSuccessful = false;
                    responseApi.errorMessage = new List<string>() { $"email or password is invalid" };
                    responseApi.statusCode = HttpStatusCode.BadRequest;

                    return BadRequest(responseApi);
                }

                responseApi.result = user;
                responseApi.statusCode = HttpStatusCode.OK;
                responseApi.isSuccessful = true;

                //Token
                string jwtToken = GenerateToken(user);

                return Ok(new { 
                    response = responseApi, 
                    token = jwtToken
                }); 
            }
            catch (Exception ex)
            {
                responseApi.isSuccessful = false;
                responseApi.errorMessage = new List<string>() { ex.ToString() };
                responseApi.statusCode = HttpStatusCode.InternalServerError;
            }
            return Ok(responseApi);
        }

        private string GenerateToken(User user)
        {
            // Jwt Configuration
            var jwt = this.configuration.GetSection("Jwt").Get<JwtModel>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("Role", user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));

            var sigin = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Create Token

            var securityToken = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: sigin
                );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
