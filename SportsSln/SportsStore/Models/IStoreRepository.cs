using System.Linq;

namespace SportsStore.Models
{
    public class IStoreRepository
    {
       public IQueryable<Product> Products { get; set; }
    }
}
