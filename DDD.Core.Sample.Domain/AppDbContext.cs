using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DDD.Core.Sample.Domain;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"server=www.shangjin666.cn,1443;database=stock_qiquan;user id=Mdkj369;password=Mdkj123;");
        }

        optionsBuilder.LogTo(Console.WriteLine);
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<MUser>().HasData(
        //    new MUser() { Id = 1, Password = "E10ADC3949BA59ABBE56E057F20F883E", Status = 1, UserName = "admin",RealName = "", AddTime = 1, Sex = 0}
        //    );
        //builder.Entity<MMenu>();
        builder.Entity<MStudent>();
        builder.Entity<MTeacher>();

        base.OnModelCreating(builder);
    }

    //public DbSet<Student> Students { get; set; }
    //public DbSet<Teacher> Teachers { get; set; }

    /// <summary>
    /// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
    /// </summary>
    private class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateOnlyConverter() : base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d))
        { }
    }
}

