using Tools.Data.Entities;
using Tools.Shared.DTOs.Compromiso;

namespace Tools.Data.Interfaces
{
    public interface ICompromisoRepository : IGenericRepository<Compromiso>
    {
        Task CreateCompromiso(Compromiso newCompromiso);
        Task<IList<Compromiso>> GetCompromisos();
        Task<Compromiso?> GetCompromisoByIdWithProveedor(int idCompromiso);
        Task<decimal> GetGastos(string fechaInicio, string fechaFin);
    }
}
