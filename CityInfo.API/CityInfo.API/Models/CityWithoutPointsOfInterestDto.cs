namespace CityInfo.API.Models
{
    /// <summary>
    /// A DTO for a city without points of interest
    /// </summary>
    public class CityWithoutPointsOfInterestDto
    {
        /// <summary>
        /// City ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// City Description
        /// </summary>
        public string? Description { get; set; }
    }
}
