using API.Entities;
using Microsoft.EntityFrameworkCore;
namespace API.Data
{
    public class DbInitializer
    {
      
        public static void Initialize(StoreContext context)
        {
            if (context.Products.Any()) return;
            var products = new List<Product>
            {
                new Product
                {
                    Id=1,
                    Name = "Angular Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    PictureUrl = "/images/products/sb-ang1.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                
            };
            foreach (var product in products)
            {
                context.Products.Add(product);
            }
           
           context.SaveChanges();
        }
    }
}
