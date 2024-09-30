using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationReflectionApi.Models;
using WebApplicationReflectionApi.Services;

namespace WebApplicationReflectionApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    #region Fields

    private readonly IService _service;

    #endregion Fields

    #region Public Constructors

    public TestController(IService service) => _service = service;

    #endregion Public Constructors

    #region Public Methods

    [HttpGet, Route("GetUserAsync")]
    public async Task<IActionResult> GetUserAsync()
    {
        var _result = await _service.GetUser();
        return Ok(_result);
    }

    [HttpGet, Route("GetUserListAsync")]
    public async Task<IActionResult> GetUserListAsync()
    {
        var _result = await _service.GetUserList();
        return Ok(_result);
    }

    #endregion Public Methods
}