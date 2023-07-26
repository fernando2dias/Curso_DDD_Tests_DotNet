using System.Net;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto, [FromServices] ILoginService service)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(loginDto is null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(loginDto);

                if(result is not null)
                {
                    return Ok(result);
                }

                return NotFound();

            }
            catch (ArgumentException e)
            {
                
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}