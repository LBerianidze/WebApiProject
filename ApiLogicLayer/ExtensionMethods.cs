using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiDataLayer.Models;
using ApiDataLayer.Models.RequestModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiLogicLayer
{
	public static class ExtensionMethods
	{
		public static User GetUser(this RegisterModel model)
		{
			User user = new User()
			{
				Email = model.Email,
				UserName = model.UserName,
				SecurityStamp = Guid.NewGuid().ToString(),
				UserAddress = new UserAddress()
				{
					City = model.City,
					Country = model.Country,
					Street = model.Street
				},
				IsMarried = model.IsMarried,
				IsWorking = model.IsWorking,
				Salary = model.Salary,
				PersonalNumber = model.PersonalNumber
			};
			return user;
		}

		public static User UpdateWithRequestData(this User user, UpdateModel model)
		{
			user.Salary = model.Salary;
			user.IsMarried = model.IsMarried;
			user.IsWorking = model.IsWorking;
			user.UserAddress.Country = model.Country;
			user.UserAddress.Street = model.Street;
			user.UserAddress.City = model.City;
			return user;
		}
		public static JwtSecurityToken GenerateToken(this User user, IConfiguration config)
		{
			List<Claim> claims = new List<Claim>
			{
				new(ClaimTypes.Name, user.UserName),
				new(ClaimTypes.Role, "User"),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};


			SymmetricSecurityKey secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtAuthentication:Secret"]));

			JwtSecurityToken jwtToken = new JwtSecurityToken(
				expires: DateTime.Now.AddDays(1),
				claims: claims,
				issuer: config["JwtAuthentication:Issuer"],
				audience: config["JwtAuthentication:Audience"],
				signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256)
			);
			return jwtToken;
		}
	}
}
