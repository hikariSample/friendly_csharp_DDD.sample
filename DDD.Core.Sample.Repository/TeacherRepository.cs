using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure.Interfaces;

namespace DDD.Core.Sample.Repository
{
    public class TeacherRepository : BaseRepository<MTeacher>, ITeacherRepository
    {
        public TeacherRepository(IDbContext dbContext): base(dbContext)
        { }
    }
}
