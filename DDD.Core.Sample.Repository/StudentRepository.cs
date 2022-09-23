using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure.Interfaces;

namespace DDD.Core.Sample.Repository
{
    public class StudentRepository : BaseRepository<MStudent>, IStudentRepository
    {
        public StudentRepository(IDbContext dbContext) : base(dbContext)
        { }

    }
}
