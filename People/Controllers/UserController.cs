using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using People.AuthenticationModels;

namespace People.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	[HttpGet("Admins")]
	[Authorize(Roles = "Administrator")]
	public IActionResult AdminsEndpoint()
	{
		var currentUser = GetCurrentUser();
		return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
	}
	
	[HttpGet("Users")]
	[Authorize(Roles = "User")]
	public IActionResult UserEndpoint()
	{
		var currentUser = GetCurrentUser();
		return Ok($"Hi {currentUser.GivenName}, you are a {currentUser.Role}");
	}
	
	
	private UserModel GetCurrentUser()
	{
		var identity = HttpContext.User.Identity as ClaimsIdentity;

		if (identity is not null)
		{
			var userClaims = identity.Claims;

			return new UserModel
			{
				Username = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value,
				EmailAddress = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value,
				GivenName = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.GivenName)?.Value,
				Surname = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Surname)?.Value,
				Role = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value
			};
		}
		return null;
	}
}