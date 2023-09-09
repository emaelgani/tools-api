using AutoMapper;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Pago;
using Tools.Shared.Exceptions;

namespace Tools.Service.Services
{
    public class PagoService : IPagoService
    {
        private readonly IMapper _mapper;
        private readonly IPagoRepository _pagoRepo;
        private readonly IClienteRepository _clienteRepo;

        public PagoService(IMapper mapper, IPagoRepository pagoRepo, IClienteRepository clienteRepo)
        {
            _mapper = mapper;
            _pagoRepo = pagoRepo;
            _clienteRepo = clienteRepo;
        }

        public async Task Add(PagoAddDTO pagoDto)
        {
            try
            {
                #region descuento el pago a la deuda del cliente
                var dbCliente = await _clienteRepo.FindByIdAsync(pagoDto.IdCliente);

                if (dbCliente == null)
                {
                    throw new NotFoundException("Cliente not found.");
                }
                dbCliente.Deuda -= pagoDto.TotalPago;
                #endregion

                var pago = _mapper.Map<Pago>(pagoDto);
                _pagoRepo.Add(pago);

                await _pagoRepo.CommitAsync();
                await _clienteRepo.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el pago", ex);
            }
        }

        public async Task<IList<PagoListDTO>> GetAllAsync()
        {
            try
            {
                return await _pagoRepo.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred getting pagos.", ex);
            }
        }

        public async Task<decimal> GetLiquidezDigital()
        {
            try
            {
                return await _pagoRepo.GetLiquidezDigital();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la liquidez digital", ex);
            }
        }

        public async Task<decimal> GetLiquidezEfectivo()
        {
            try
            {
                return await _pagoRepo.GetLiquidezEfectivo();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la liquidez efectivo", ex);
            }
        }

        public async Task<IList<PagoPorMesDTO>> GetPagosPorMes()
        {
            try
            {
                return await _pagoRepo.GetPagosPorMes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pagos por mes.", ex);
            }
        }

        public async Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes()
        {
            try
            {
                return await _pagoRepo.GetPagosYVentasPorMes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener pagos y ventas por mes.", ex);
            }
        }
    }
}
