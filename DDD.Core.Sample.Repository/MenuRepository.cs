using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Domain;

namespace DDD.Core.Sample.Repository;

/// <summary>
/// 菜单仓储类
/// </summary>
public class MenuRepository : BaseRepository<MMenu>, IMenuRepository
{
    public MenuRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}