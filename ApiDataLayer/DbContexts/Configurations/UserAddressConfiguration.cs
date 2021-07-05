using System;
using System.Collections.Generic;
using System.Text;
using ApiDataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDataLayer.DbContexts.Configurations
{
	public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
	{
		public void Configure(EntityTypeBuilder<UserAddress> modelBuilder)
		{
			modelBuilder.HasKey(t => t.Id);
			modelBuilder.Property(t => t.City).IsRequired();
			modelBuilder.Property(t => t.Country).IsRequired();
			modelBuilder.Property(t => t.Street).IsRequired().HasMaxLength(32);
		}
	}
}
