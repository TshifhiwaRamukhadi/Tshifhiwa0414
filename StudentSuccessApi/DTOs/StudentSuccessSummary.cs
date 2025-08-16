namespace StudentSuccessApi.DTOs
{
	public class StudentSuccessSummary
	{
		public int StudentId { get; set; }
		public double Score { get; set; }
		public string Band { get; set; } = string.Empty;
		public Dictionary<string, double> Breakdown { get; set; } = new();
		public List<string> Notes { get; set; } = new();
	}
}