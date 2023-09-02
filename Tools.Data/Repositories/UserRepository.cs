using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(HerramientasDbContext db) : base(db)
        {
        }
    }
}
