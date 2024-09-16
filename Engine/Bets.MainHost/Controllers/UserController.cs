
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger) {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public User GetUserProfile(int id) {
        return _userService.GetUserProfile(id);
    }

    [HttpPost("update")]
    public IActionResult UpdateUserProfile(User user) {
        _userService.UpdateUserProfile(user);
        return Ok("Профиль обновлен");
    }
}
