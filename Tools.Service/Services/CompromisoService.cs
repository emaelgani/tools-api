using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Cliente;
using Tools.Shared.DTOs.Compromiso;
using Tools.Shared.Exceptions;

namespace Tools.Service.Services
{
    public class CompromisoService : ICompromisoService
    {
        private readonly IMapper _mapper;
        private readonly ICompromisoRepository _compromisoRepo;

        public CompromisoService(IMapper mapper, ICompromisoRepository compromisoRepo)
        {
            _mapper = mapper;
            _compromisoRepo = compromisoRepo;
        }

        public async Task CreateCompromiso(CompromisoDTO newCompromiso)
        {
            try
            {
                var compromiso = _mapper.Map<Compromiso>(newCompromiso);
                await _compromisoRepo.CreateCompromiso(compromiso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteCompromiso(int idCompromiso)
        {
            try
            {
                await _compromisoRepo.RemoveByIdAsync(idCompromiso);
                await _compromisoRepo.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CompromisoDTO> GetCompromisoById(int idCompromiso)
        {
            try
            {
                var compromiso = await _compromisoRepo.GetCompromisoByIdWithProveedor(idCompromiso);
                var compromisoDTO = _mapper.Map<CompromisoDTO>(compromiso);
                return compromisoDTO;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<CompromisoDTO>> GetCompromisos()
        {
            try
            {
                var compromisos = await _compromisoRepo.GetCompromisos();
                var compromisosDTO = _mapper.Map<IList<CompromisoDTO>>(compromisos);
                return compromisosDTO;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetCompromisosNoSaldadosDelDia()
        {
            try
            {
                var today = DateTime.Today;

                var totalCompromisos = await _compromisoRepo
                    .FindByCondition(p => !p.Estado && p.Fecha.Date == today)
                    .CountAsync();

                return totalCompromisos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los compromisos no saldados del día", ex);
            }
        }

        public async Task<decimal> GetGastos(string fechaInicio, string fechaFin)
        {
            try
            {
                return await _compromisoRepo.GetGastos(fechaInicio, fechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<decimal> GetMontoTotal()
        {
            try
            {
                var deuda = await _compromisoRepo.FindByCondition(p => !p.Estado).SumAsync(p => p.Monto);
                return deuda;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el monto total de los compromisos no pagados.", ex);
            }
        }

        public async Task<decimal> GetTotalDigital()
        {
            try
            {
                var totalMontoDigital = await _compromisoRepo.FindByCondition(p => p.Estado).SumAsync(p => p.PagoDigital);
                return totalMontoDigital ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el monto total de los compromisos en digital", ex);
            }
        }

        public async Task<decimal> GetTotalEfectivo()
        {
            try
            {
                var totalMontoEfectivo = await _compromisoRepo.FindByCondition(p => p.Estado).SumAsync(p => p.PagoEfectivo);
                return totalMontoEfectivo ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el monto total de los compromisos en efectivo", ex);
            }
        }

        public async Task UpdateCompromiso(CompromisoDTO updateCompromiso)
        {
            try
            {
                var existingCompromiso = await _compromisoRepo.FindByIdAsync(updateCompromiso.IdCompromiso);

                if (existingCompromiso is null)
                {
                    throw new NotFoundException("Compromiso not found.");
                }

                _mapper.Map(updateCompromiso, existingCompromiso);

                _compromisoRepo.Update(existingCompromiso);
                await _compromisoRepo.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating compromiso.", ex);
            }
        }
    }
}
