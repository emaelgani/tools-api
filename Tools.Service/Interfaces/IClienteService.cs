using Tools.Shared.DTOs.Cliente;

namespace Tools.Service.Interfaces
{
    public interface IClienteService
    {
        /// <summary>
        /// List of clients.
        /// </summary>
        public Task<IList<ClienteDTO>> GetAllAsync();

        /// <summary>
        /// Add new client.
        /// </summary>
        /// <param name="clienteDto"></param>
        public Task Add(ClienteDTO clienteDto);

        /// <summary>
        /// Update a client.
        /// </summary>
        /// <param name="clienteDto"></param>
        public Task Update(ClienteDTO clienteDto);

        /// <summary>
        /// Return sum all debt clients.
        /// </summary>
        /// <returns></returns>
        public Task<decimal> GetTotalDebt();

        /// <summary>
        /// Return four client with more debt.
        /// </summary>
        /// <returns></returns>
        public Task<IList<ClienteDTO>> GetFourMoreDebt();

        /// <summary>
        /// Devuelve los clientes que compraron el producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public Task<IList<ClienteVentaProductoDTO>> GetClientByIdProduct(int idProducto);
    }
}
