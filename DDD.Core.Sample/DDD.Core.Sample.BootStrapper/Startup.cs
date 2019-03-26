using System;
using DDD.Core.Sample.Application;
using DDD.Core.Sample.Infrastructure;
using DDD.Core.Sample.Infrastructure.Interfaces;
using DDD.Core.Sample.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Extensions.Logging;

namespace DDD.Core.Sample.BootStrapper  //引导程序
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        public static void Configure()
        {
            //configure ioc
            ConfigureMapper();
        }

        public static void AddConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContextPool<SchoolDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"), b => b.UseRowNumberForPaging())); //数据库连接注入
            services.AddTransient(typeof(IDbContext), typeof(SchoolDbContext));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.Scan(selector => selector.FromAssembliesOf(typeof(BaseRepository<>)).AddClasses().AsImplementedInterfaces().WithScopedLifetime()); //注入所有业务
            services.Scan(selector => selector.FromAssembliesOf(typeof(BaseService<>)).AddClasses().AsImplementedInterfaces().WithScopedLifetime()); //注入所有业务
            services.AddTransient(typeof(Lazy<>));//注册Lazy
        }

        public static void AddConfigure(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);//这是为了防止中文乱码
            loggerFactory.AddNLog();//添加NLog
            NLog.LogManager.LoadConfiguration("nlog.config");  //读取Nlog配置文件
        }

        private static void ConfigureMapper()
        {
            //configure automapper dtos
        }
    }
}
