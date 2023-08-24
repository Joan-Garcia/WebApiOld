using Microsoft.AspNetCore.Mvc;
using WebApi.DataAcesss;
using WebApi.DataTransfer;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly ILogger<UserController> _logger;
    private readonly UserDao _userDao;

    public UserController(ILogger<UserController> logger, UserDao userDao) {
        _logger = logger;
        _userDao = userDao;
    }

    [HttpGet("AllUsers")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUser() {
        _logger.LogInformation("Getting all users");
        return Ok(await _userDao.GetUsers());
    }

    [HttpGet("{IdPersonal}")]
    public async Task<ActionResult<User>> GetUser(int IdPersonal) {
        _logger.LogInformation($"Getting user with IdPersonal: {IdPersonal}");

        var user = await _userDao.GetUser(IdPersonal);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateUser(UserDto user) {
        _logger.LogInformation($"Creating user with IdPersonal: {user.IdPersonal}");
        return Ok(await _userDao.CreateUser(user));
    }

    [HttpPut]
    public async Task<ActionResult<int>> UpdateUser(UserDto user) {
        _logger.LogInformation($"Updating user with IdPersonal: {user.IdPersonal}");
        return Ok(await _userDao.UpdateUser(user));
    }

    [HttpDelete("{IdPersonal}")]
    public async Task<ActionResult<int>> DeleteUser(int IdPersonal) {
        _logger.LogInformation($"Deleting user with IdPersonal: {IdPersonal}");
        return Ok(await _userDao.DeleteUser(IdPersonal));
    }
}