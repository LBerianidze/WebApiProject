using Microsoft.AspNetCore.Identity;

namespace ApiDataLayer.Models
{
	public class User : IdentityUser
	{
		public string PersonalNumber { get; set; }
		public bool IsMarried { get; set; }
		public bool IsWorking { get; set; }
		public double? Salary { get; set; }
		public UserAddress UserAddress { get; set; }
	}
}
