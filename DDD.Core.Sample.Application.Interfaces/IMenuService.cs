using DDD.Core.Sample.Application.DTO;

namespace DDD.Core.Sample.Application.Interfaces;
/// <summary>
/// 菜单业务接口
/// </summary>
public interface IMenuService : IBaseService
{
    /// <summary>
    /// 获得列表
    /// </summary>
    Task<List<MenuDto>> GetListAsync();
    /// <summary>
    /// 获得详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MenuDto> GetAsync(long id);
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(MenuDto dto);
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<long> AddAsync(MenuDto dto);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id);
}