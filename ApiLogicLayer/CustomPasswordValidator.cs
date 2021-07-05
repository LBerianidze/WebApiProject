using ApiDataLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ApiLogicLayer
{
	public class CustomPasswordValidator : IPasswordValidator<User>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
		{
			return Task.FromResult(IdentityResult.Success);
		}
	}
}
