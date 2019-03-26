using DDD.Core.Sample.Domain;
using DDD.Core.Sample.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace DDD.Core.Sample.Infrastructure
{
    public class SchoolDbContext : DbContext, IDbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"server=www.shangjin666.cn,1443;database=stock_qiquan;user id=Mdkj369;password=Mdkj123;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            Type type = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().DeclaringType;  //当前类型
            Assembly assembyle = Assembly.GetAssembly(type);  //查找类库
            var typeList = assembyle.GetTypes().Where(t => ((TypeInfo)t).ImplementedInterfaces.Contains(typeof(IAggregateRoot)));  //查找该命名空间下实现了接口的所有类型
            foreach (Type type1 in typeList)
            {
                builder.Entity(type1);
            }
            //builder.Entity<Student>();
            // builder.Entity<Teacher>();

            base.OnModelCreating(builder);
        }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Teacher> Teachers { get; set; }

    }
}
