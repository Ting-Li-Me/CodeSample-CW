using DataEF.Repositoies;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace DataEF.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private ProductContext context;

        public UnitOfWork(ProductContext context)
        {
            this.context = context;
            Product = new ProductRepository(this.context);
        }

        public IProductRepository Product { get; private set; }

        public async Task SaveAsync()
        {
             await context.SaveChangesAsync();
        }
    }
}
