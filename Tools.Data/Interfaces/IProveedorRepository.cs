using Tools.Data.Entities;

namespace Tools.Data.Interfaces
{
    public interface IProveedorRepository : IGenericRepository<Proveedor>
    {

        Task<Proveedor?> GetProveedorWithProductsAsync(int id);

        Task<IList<Proveedor>?> GetAllProveedorWithProductsAsync();
    }
}
