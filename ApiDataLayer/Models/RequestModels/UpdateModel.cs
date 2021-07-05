using FluentValidation;

namespace ApiDataLayer.Models.RequestModels
{
	public class UpdateModel
	{
		public bool IsMarried { get; set; }
		public bool IsWorking { get; set; }
		public double? Salary { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
	}
	public class UpdateModelValidator : AbstractValidator<UpdateModel>
	{
		public UpdateModelValidator()
		{
			RuleFor(x => x.Salary).Must(t => t >= 0);
			RuleFor(x => x.Country).NotEmpty().MinimumLength(3);
			RuleFor(x => x.City).NotEmpty().MinimumLength(3);
			RuleFor(x => x.Street).NotEmpty().MinimumLength(10).MaximumLength(32);
		}
	}
}
