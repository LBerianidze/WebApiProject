using ApiDataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDataLayer.DbContexts.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> modelBuilder)
		{
			modelBuilder.HasKey(t => t.Id);

			modelBuilder.HasOne(u => u.UserAddress)
				.WithOne(a => a.User)
				.HasForeignKey<UserAddress>(a => a.UserId).
				OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Property(t => t.Email).IsRequired();
			modelBuilder.Property(t => t.UserName).IsRequired();
			modelBuilder.Property(t => t.PasswordHash).IsRequired();
			modelBuilder.Property(t => t.PersonalNumber).HasMaxLength(11).IsFixedLength(true);
		}
	}
}
