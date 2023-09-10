using Tools.Shared.DTOs.Compromiso;

namespace Tools.Service.Interfaces
{
    public interface ICompromisoService
    {
        Task CreateCompromiso(CompromisoDTO newCompromiso);
        Task<IList<CompromisoDTO>> GetCompromisos();
        Task<CompromisoDTO> GetCompromisoById(int idCompromiso);
        Task UpdateCompromiso(CompromisoDTO updateCompromiso);
        Task DeleteCompromiso(int idCompromiso);
        Task<decimal> GetMontoTotal();
        Task<decimal> GetTotalEfectivo();
        Task<decimal> GetTotalDigital();
        Task<int> GetCompromisosNoSaldadosDelDia();
        Task<decimal> GetGastos(string fechaInicio, string fechaFin);

    }
}
