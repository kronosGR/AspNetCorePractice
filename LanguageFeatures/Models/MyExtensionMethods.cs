using System.Collections.Generic;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
        {
            public static decimal TotalPrices(this ShoppingCart cartParam)
            {
                decimal total = 0;
                foreach (Product prod in cartParam.Products)
                {
                    total += prod?.Price ?? 0;
                }

                return total;
            }

            public static IEnumerable<Product> FilterByPrice(this IEnumerable<Product> productEnum,
                decimal minumumPrice)
            {
                foreach (Product prod in productEnum)
                {
                    if ((prod?.Price ?? 0) >= minumumPrice)
                    {
                        yield return prod;
                    }
                }
            }
    }
}
