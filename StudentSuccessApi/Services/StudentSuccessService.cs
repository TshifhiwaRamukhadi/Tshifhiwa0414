using StudentSuccessApi.Models;
using StudentSuccessApi.DTOs;

namespace StudentSuccessApi.Services
{
	public class StudentSuccessService
	{
		public StudentSuccessSummary Calculate(StudentProfile student)
		{
			double gpaComponent = Normalize(student.Gpa, 0, 4) * 100.0;
			double attendanceComponent = student.AttendancePercent;
			double creditsProgress = student.TotalCreditsRequired > 0
				? Math.Clamp((double)student.CompletedCredits / student.TotalCreditsRequired, 0.0, 1.0) * 100.0
				: 0.0;
			double lastTermComponent = Normalize(student.LastTermGpa, 0, 4) * 100.0;
			double extracurricularComponent = Math.Clamp(student.ExtracurricularPoints / 10.0, 0.0, 1.0) * 100.0;

			double score =
				0.40 * gpaComponent +
				0.25 * attendanceComponent +
				0.20 * creditsProgress +
				0.10 * lastTermComponent +
				0.05 * extracurricularComponent;

			score = Math.Round(score, 2);

			string band = score >= 85 ? "Excellent"
				: score >= 70 ? "Good"
				: score >= 50 ? "Needs Support"
				: "At Risk";

			return new StudentSuccessSummary
			{
				StudentId = student.Id,
				Score = score,
				Band = band,
				Breakdown = new Dictionary<string, double>
				{
					{"Gpa", Math.Round(0.40 * gpaComponent, 2)},
					{"Attendance", Math.Round(0.25 * attendanceComponent, 2)},
					{"CreditsProgress", Math.Round(0.20 * creditsProgress, 2)},
					{"LastTerm", Math.Round(0.10 * lastTermComponent, 2)},
					{"Extracurricular", Math.Round(0.05 * extracurricularComponent, 2)}
				},
				Notes = BuildNotes(student)
			};
		}

		private static double Normalize(double value, double min, double max)
		{
			if (max <= min) return 0.0;
			return Math.Clamp((value - min) / (max - min), 0.0, 1.0);
		}

		private static List<string> BuildNotes(StudentProfile s)
		{
			var notes = new List<string>();
			if (s.Gpa < 2.0) notes.Add("GPA below 2.0 – consider tutoring support.");
			if (s.AttendancePercent < 75) notes.Add("Attendance under 75% – at-risk of falling behind.");
			if (s.CompletedCredits < s.TotalCreditsRequired / 2) notes.Add("Credit progress under 50%.");
			if (s.LastTermGpa < s.Gpa) notes.Add("Recent GPA dipped – monitor trend.");
			if (s.ExtracurricularPoints == 0) notes.Add("No extracurriculars – consider engagement for growth.");
			if (!notes.Any()) notes.Add("Strong overall performance – keep it up!");
			return notes;
		}
	}
}