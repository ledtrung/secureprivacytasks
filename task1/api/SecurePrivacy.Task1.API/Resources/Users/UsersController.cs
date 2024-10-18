using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace SecurePrivacy.Task1.API.Resources.Users;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly UserService _userService;

    public UsersController(ILogger<UsersController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var users = await _userService.GetUsers();
            return Ok(users.Select(e => UserDTO.FromUser(e)).ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Contact support");
        }
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        try
        {
            var user = await _userService.GetUser(id);
            if (user is null)
                return NotFound();
                
            return Ok(UserDTO.FromUser(user));
        
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
        catch (Exception ex)
        { 
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Contact support");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> Add(UserDTO user)
    {
        try
        {
            await _userService.AddUser(user.ToUser());
            
            return Created();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(501, "Update not implemented");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "Contact support");
        }
    }
}
