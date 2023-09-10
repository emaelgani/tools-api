using Tools.Data.Entities;
using Tools.Shared.DTOs.Cliente;

namespace Tools.Data.Interfaces
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<IList<ClienteVentaProductoDTO>> GetClientesVentaByProducto(int IdProducto);
    }
}
