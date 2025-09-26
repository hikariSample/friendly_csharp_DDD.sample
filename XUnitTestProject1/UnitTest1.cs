using DDD.Core.Sample.Application;
using DDD.Core.Sample.Application.Interfaces;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private IStudentService _studentService;

        public UnitTest1()
        {
            string connectionStrings = "";
            IServiceCollection services = new ServiceCollection();
            services.AddEntityFrameworkSqlServer().AddDbContext<SchoolDbContext>(options => options.UseSqlServer(connectionStrings, b => b.EnableRetryOnFailure()), ServiceLifetime.Transient);
						services.AddTransient(typeof(IDbContext),typeof(SchoolDbContext));
						services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            _studentService = serviceProvider.GetService(typeof(IStudentService)) as IStudentService;
        }
        [Fact]
        public void Test1()
        {
            var student = _studentService.GetAsync(1).Result;
            Assert.NotNull(student);
        }
    }
}
