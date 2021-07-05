using System.Reflection;
using ApiDataLayer.DbContexts.Configurations;
using ApiDataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiDataLayer.DbContexts
{
	public class UserApiDbContext : IdentityDbContext<User>
	{
		public UserApiDbContext(DbContextOptions<UserApiDbContext> ctx) : base(ctx)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
		public DbSet<User> Users { get; set; }
		public DbSet<UserAddress> UserAddresses { get; set; }
	}
}
