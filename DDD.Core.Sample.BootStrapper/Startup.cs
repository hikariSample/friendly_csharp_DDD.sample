using DDD.Core.Sample.Application;
using DDD.Core.Sample.Domain;
using DDD.Core.Sample.Infrastructure;
using DDD.Core.Sample.Infrastructure.Interfaces;
using DDD.Core.Sample.Repository;
using Hikari.Common.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddDbContextPool<AppDbContext>(options => options.SetOptionsBuilder(configuration.GetConnectionString("Default"), DbTypeEnum.SqlServer, nameof(DDD.Core.Sample.Domain))); //数据库连接注入
            services.AddScoped(typeof(IDbContext), typeof(AppDbContext));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.Scan(selector => selector.FromAssembliesOf(typeof(BaseRepository<>)).AddClasses().AsImplementedInterfaces().WithScopedLifetime()); //注入所有仓储
            services.Scan(selector => selector.FromAssembliesOf(typeof(BaseService)).AddClasses().AsImplementedInterfaces().WithScopedLifetime()); //注入所有业务
            services.AddTransient(typeof(Lazy<>));//注册Lazy
            services.AddSingleton<IConfiguration>(configuration);
        }

        public static void AddConfigure()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);//这是为了防止中文乱码
            NLog.Web.NLogBuilder.ConfigureNLog(System.IO.Path.Combine(System.Environment.CurrentDirectory, "nlog.config"));//读取Nlog配置文件  //读取Nlog配置文件
        }

        private static void ConfigureMapper()
        {
            //configure automapper dtos
        }
    }
}
