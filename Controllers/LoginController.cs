using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/login")]
[Authorize]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login()
    {
        return Ok();
    }
}