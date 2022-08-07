using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductBO>> GetAllProductsAsync();
        Task<ProductBO> GetProdByidAsync(int id);
        Task AddProdAsync(ProductBO productBO);
        Task UpdateProdAsync(ProductBO productBO);
        Task DeleteProdAsync(int id);

    }
}
