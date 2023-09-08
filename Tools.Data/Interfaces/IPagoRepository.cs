using Tools.Data.Entities;

namespace Tools.Data.Interfaces
{
    public interface IPagoRepository : IGenericRepository<Pago>
    {
        public Task<decimal> GetLiquidezDigital();
        public Task<decimal> GetLiquidezEfectivo();
    }
}
