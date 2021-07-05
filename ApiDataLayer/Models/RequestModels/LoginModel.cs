using FluentValidation;

namespace ApiDataLayer.Models.RequestModels
{
	public class LoginModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
	public class LoginModelValidator : AbstractValidator<LoginModel>
	{
		public LoginModelValidator()
		{
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
		}
	}
}
