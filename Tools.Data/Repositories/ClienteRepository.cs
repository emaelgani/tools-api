using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(HerramientasDbContext db) : base(db)
        {
        }
    }
}
