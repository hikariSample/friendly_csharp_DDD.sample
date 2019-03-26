using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DDD.Core.Sample.Domain;

namespace DDD.Core.Sample.Application.Interfaces
{
    /// <summary>
    /// 业务接口基类
    /// </summary>
    /// <typeparam name="TAggregateRoot">类型</typeparam>
    public interface IBaseService<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        
    }
}