using FluentValidation;

namespace ApiDataLayer.Models.RequestModels
{
	public class RegisterModel
	{
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string PersonalNumber { get; set; }
		public bool IsMarried { get; set; }
		public bool IsWorking { get; set; }
		public double? Salary { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
	}
	public class RegisterModelValidator : AbstractValidator<RegisterModel>
	{
		public RegisterModelValidator()
		{
			RuleFor(x => x.Email).Custom((email, ctx) =>
			{
				//Easy check for sample
				//but we can check with regex here
				if (email == null)
				{
					ctx.AddFailure("No email entered");
					return;
				}

				if (!email.Contains("@"))
				{
					ctx.AddFailure("Bad email");
				}
			});
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
			RuleFor(x => x.PersonalNumber).NotEmpty().Length(11);
			RuleFor(x => x.Salary).Must(t => t >= 0);
			RuleFor(x => x.Country).NotEmpty().MinimumLength(3);
			RuleFor(x => x.City).NotEmpty().MinimumLength(3);
			RuleFor(x => x.Street).NotEmpty().MinimumLength(10).MaximumLength(32);
		}
	}
}
