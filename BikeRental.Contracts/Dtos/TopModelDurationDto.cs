namespace BikeRental.Contracts.Dtos
{
	/// <summary>
	/// Data transfer object representing total rental duration for a specific bike model.
	/// </summary>
	public class TopModelDurationDto
	{
		/// <summary>
		/// Name of the bike model.
		/// </summary>
		public string Model { get; set; } = string.Empty;

		/// <summary>
		/// Total rental duration for the model in hours.
		/// </summary>
		public int TotalHours { get; set; }
	}
}
