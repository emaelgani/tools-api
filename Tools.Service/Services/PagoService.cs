using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.DTOs.Pago;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

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

        public async Task Add(PagoDTO pagoDto)
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

        public Task<IList<PagoDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
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

        public Task<IList<PagoPorMesDTO>> GetPagosPorMes()
        {
            throw new NotImplementedException();
        }

        public Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes()
        {
            throw new NotImplementedException();
        }
    }
}
