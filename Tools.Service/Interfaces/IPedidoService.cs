using Tools.Shared.DTOs.Pedido;

namespace Tools.Service.Interfaces
{
    public interface IPedidoService
    {
        Task<IList<PedidoWithClientDTO>> GetPedidos();
        Task UpdatePedido(PedidoDTO updatePedido);
        Task CreatePedido(PedidoDTO newPedido);
        Task DeletePedido(int id);
        Task ChangeStatus(int id);
    }
}
