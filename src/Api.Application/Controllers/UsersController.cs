using System.Net;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        if (!ModelState.IsValid)
        {//400
            return BadRequest(ModelState);
        }

        try
        {//200
            return Ok(await _service.GetAll());
        }
        catch (ArgumentException e)
        {//500
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }


    }

}