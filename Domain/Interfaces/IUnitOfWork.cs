using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        Task SaveAsync();
    }
}
