using DDD.Core.Sample.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Core.Sample.Application.Interfaces
{
    public interface IStudentService : IBaseService<Student>
    {
        Task<Student> GetAsync(int id);

        Task<bool> AddAsync(string name);
    }
}
