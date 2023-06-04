using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
  public class BidEntity
  {
    public int Id { get; set; }
    public int HouseId { get; set; }
    public HouseEntity? House { get; set; }
    public string Bidder { get; set; }
    public int Amount { get; set; }
  }
}