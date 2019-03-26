using DDD.Core.Sample.Application.Interfaces;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DDD.Core.Sample.Domain;
using DDD.Core.Sample.Infrastructure;
using DDD.Core.Sample.Infrastructure.Interfaces;

namespace DDD.Core.Sample.Application
{
    /// <summary>
    /// 业务基类
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    public class BaseService<TAggregateRoot> : IBaseService<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
    }
}