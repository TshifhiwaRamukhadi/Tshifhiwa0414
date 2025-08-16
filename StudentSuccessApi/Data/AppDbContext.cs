using Microsoft.EntityFrameworkCore;
using StudentSuccessApi.Models;

namespace StudentSuccessApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<StudentProfile>()
				.HasIndex(s => s.Email)
				.IsUnique();

			modelBuilder.Entity<StudentProfile>()
				.Property(s => s.Gpa)
				.HasPrecision(3, 2);

			modelBuilder.Entity<StudentProfile>()
				.Property(s => s.LastTermGpa)
				.HasPrecision(3, 2);
		}
	}
}