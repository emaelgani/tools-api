using AutoMapper;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.DTOs.Pedido;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

namespace Tools.Service.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepo;

        public PedidoService(IMapper mapper, IPedidoRepository pedidoRepo)
        {
            _mapper = mapper;
            _pedidoRepo = pedidoRepo;
        }

        public async Task ChangeStatus(int id)
        {
            try
            {
                var dbPedido = await _pedidoRepo.FindByIdAsync(id);

                if (dbPedido is null)
                {
                    throw new NotFoundException("Pedido not found.");
                }

                dbPedido.Estado = !dbPedido.Estado;

                await _pedidoRepo.CommitAsync();
            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del pedido", ex);
            }
        }

        public async Task CreatePedido(PedidoDTO newPedido)
        {
            try
            {
                var pedido = _mapper.Map<Pedido>(newPedido);

                // Eliminar la parte de la hora
                pedido.Fecha = pedido.Fecha.Date;

                _pedidoRepo.Add(pedido);

                await _pedidoRepo.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try adding a new pedido", ex);

            }
        }

        public async Task DeletePedido(int id)
        {
            try
            {
                var dbPedido = await _pedidoRepo.FindByIdAsync(id);

                if (dbPedido == null)
                {
                    throw new NotFoundException("Pedido not found.");
                }

                _pedidoRepo.Remove(dbPedido);

                await _pedidoRepo.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred deleting pedido.", ex);
            }
        }

        public async Task<IList<PedidoWithClientDTO>> GetPedidos()
        {
            try
            {
                return _mapper.Map<IList<PedidoWithClientDTO>>(await _pedidoRepo.GetPedidosWithClientAsync());
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred getting pedidos.", ex);

            }
        }

        public Task UpdatePedido(PedidoDTO updatePedido)
        {
            throw new NotImplementedException();
        }
    }
}
