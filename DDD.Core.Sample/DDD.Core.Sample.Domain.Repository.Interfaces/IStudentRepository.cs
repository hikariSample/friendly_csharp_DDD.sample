﻿using System.Linq;
using DDD.Core.Sample.Domain.Entity;

namespace DDD.Core.Sample.Domain.Repository.Interfaces
{
    /// <summary>
    /// 仓储接口示例
    /// </summary>
    public interface IStudentRepository : IBaseRepository<Student>
    {
    }
}
