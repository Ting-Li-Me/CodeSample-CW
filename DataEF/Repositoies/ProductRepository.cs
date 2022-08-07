using Domain.Interfaces;
using Domain.Models;

namespace DataEF.Repositoies
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context) { }
    }
}
