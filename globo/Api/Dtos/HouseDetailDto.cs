using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
  public record HouseDetailDto(int Id, string? Address, string? country,
      int Price, string? Description, string? Photo);
}