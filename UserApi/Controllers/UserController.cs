using ApiDataLayer.DbContexts;
using ApiDataLayer.Models;
using ApiDataLayer.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiLogicLayer;

namespace UserApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IConfiguration config;
		private readonly UserManager<User> userManager;
		private readonly UserApiDbContext db;
		public UserController(UserManager<User> manager, UserApiDbContext db, IConfiguration config)
		{
			userManager = manager;
			this.config = config;
			this.db = db;
		}
		/// <summary>
		/// Create new user
		/// </summary>
		/// <param name="model">Register fields</param>
		/// <returns></returns>
		[HttpPost]
		[Route("Register")]

		public async Task<IActionResult> Register([FromBody] RegisterModel model)
		{
			if (await userManager.FindByNameAsync(model.UserName) != null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new Response { ErrorCode = 2, Status = "Error", Description = "Login exists" });
			}

			IdentityResult result = await userManager.CreateAsync(model.GetUser(), model.Password);
			if (!result.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { ErrorCode = 3, Status = "Error", Description = "Server side error" });
			}

			return Ok(new Response { ErrorCode = 0, Status = "Success" });
		}
		/// <summary>
		/// Authentification
		/// </summary>
		/// <param name="model">Login fields</param>
		/// <returns></returns>
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginModel model)
		{
			User user = await userManager.FindByNameAsync(model.UserName);
			if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
			{
				return StatusCode(StatusCodes.Status400BadRequest, new Response { ErrorCode = 4, Status = "Error", Description = "Wrong credentials" });
			}

			var jwtToken = user.GenerateToken(config);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
				expiration = jwtToken.ValidTo
			});

		}
		/// <summary>
		/// Delete user
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[Route("delete")]
		[HttpDelete]
		public async Task<IActionResult> Delete()
		{
			var identity = User.Identity;
			string login = identity.Name;
			User user = await db.Users.FirstOrDefaultAsync(t => t.UserName == login);
			if (user == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new Response { ErrorCode = 4, Status = "Error", Description = "Wrong credentials" });
			}
			await userManager.DeleteAsync(user);

			return StatusCode(StatusCodes.Status200OK, new Response { ErrorCode = 0, Status = "Success" });
		}
		/// <summary>
		/// Update user information
		/// </summary>
		/// <param name="model">Update fields</param>
		/// <returns></returns>
		[Authorize]
		[Route("update")]
		[HttpPost]
		public async Task<IActionResult> Update([FromBody]UpdateModel model)
		{

			var identity = User.Identity;
			string login = identity.Name;
			User user = await db.Users.Include(t => t.UserAddress).FirstOrDefaultAsync(t => t.UserName == login);
			if (user == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new Response { ErrorCode = 4, Status = "Error", Description = "Wrong credentials" });
			}

			user.UpdateWithRequestData(model);

			IdentityResult updateResult = await userManager.UpdateAsync(user);
			if (!updateResult.Succeeded)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new Response { ErrorCode = 3, Status = "Error", Description = "Server side error" });
			}

			return StatusCode(StatusCodes.Status200OK, new Response { ErrorCode = 0, Status = "Success" });
		}
	}
}
