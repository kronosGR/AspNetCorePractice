using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
  public record BidDto(int Id, int HouseId, [property: Required] string Bidder, int amount);
}