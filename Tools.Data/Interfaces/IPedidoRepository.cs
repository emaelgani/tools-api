using Tools.Data.Entities;

namespace Tools.Data.Interfaces
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        Task<IList<Pedido>> GetPedidosWithClientAsync();
    }
}
