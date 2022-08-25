using System.Linq;

namespace SportsStore.Models
{
    public class IStoreRepository
    {
        IQueryable<Product> Products { get; set; }
    }
}
