using System.ComponentModel.DataAnnotations;

public record HouseDto(int Id, [property: Required] string? Address,
   [property: Required] string? Country, int Price);