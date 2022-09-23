

using System;
using System.Linq;
using System.Reflection;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure;
using DDD.Core.Sample.Infrastructure.Interfaces;

namespace DDD.Core.Sample.Repository
{
    /// <summary>
    /// 仓储工厂类
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// 工厂创建仓储
        /// </summary>
        /// <typeparam name="T">需要创建的类型</typeparam>
        /// <param name="dbContext">数据访问上下文</param>
        /// <returns></returns>
        public static T CreateObj<T>(IDbContext dbContext) where T : class
        {
            Type type = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().DeclaringType;
            Assembly assembyle = Assembly.GetAssembly(type);  //查找所有类型
            var typeList = assembyle.GetTypes().Where(t => t.Namespace == type.Namespace);  //查找该命名空间下的所有类型
            type = typeList.FirstOrDefault(t => t.FullName == typeof(T).FullName);  //查找需要实例化的类型
            object[] parameters = new object[1];
            parameters[0] = dbContext;  //构建构造参数
            return (T)Activator.CreateInstance(type, parameters);  //实例化对象
        }
    }
}