using System.ComponentModel.DataAnnotations;

namespace StudentSuccessApi.Models
{
	public class StudentProfile
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[MaxLength(100)]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		[MaxLength(200)]
		public string Email { get; set; } = string.Empty;

		public DateTime? DateOfBirth { get; set; }

		public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

		[MaxLength(100)]
		public string? Major { get; set; }

		[Range(0, 4)]
		public double Gpa { get; set; }

		[Range(0, 100)]
		public int AttendancePercent { get; set; }

		[Range(0, int.MaxValue)]
		public int CompletedCredits { get; set; }

		[Range(1, int.MaxValue)]
		public int TotalCreditsRequired { get; set; } = 120;

		[Range(0, 100)]
		public int ExtracurricularPoints { get; set; }

		[Range(0, 4)]
		public double LastTermGpa { get; set; }

		public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

		public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
	}
}