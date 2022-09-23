using DDD.Core.Sample.Application.DTO;
using DDD.Core.Sample.Application.Interfaces;
using DDD.Core.Sample.Domain.Entity;
using DDD.Core.Sample.Domain.Repository.Interfaces;
using DDD.Core.Sample.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDD.Core.Sample.Application;
/// <summary>
/// 菜单业务类
/// </summary>
public class MenuService : BaseService, IMenuService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMenuRepository _menuRepository;  // 菜单仓储接口
    public MenuService(IUnitOfWork unitOfWork, IMenuRepository menuRepository)
    {
        _unitOfWork = unitOfWork;
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuDto>> GetListAsync()
    {
        var models = await _menuRepository.FindList(b => !b.IsDeleted).OrderBy(x => x.Sort).ToListAsync();
        var dtos = models.Select(model => new MenuDto()
        {
            Id = model.Id,
            ParentId = model.ParentId,
            Name = model.Name,
            Icon = model.Icon,
            OpenStyle = model.OpenStyle,
            Type = model.Type,
            Url = model.Url,
            Sort = model.Sort
        }).ToList();

        return dtos;
    }

    public async Task<MenuDto> GetAsync(long id)
    {
        var model = await _menuRepository.FindAsync(x => x.Id == id);
        var dto = new MenuDto()
        {
            Type = model.Type,
            Icon = model.Icon,
            Id = model.Id,
            Name = model.Name,
            Sort = model.Sort,
            OpenStyle = model.OpenStyle,
            ParentId = model.ParentId,
            Url = model.Url
        };

        return dto;
    }

    public async Task<bool> UpdateAsync(MenuDto dto)
    {
        var model = await _menuRepository.FindAsync(x => x.Id == dto.Id);
        if (model is null) return false;
        model.Sort = dto.Sort;
        model.Icon = dto.Icon;
        model.Name = dto.Name;
        model.OpenStyle = dto.OpenStyle;
        model.ParentId = dto.ParentId;
        model.Type = dto.Type;
        model.Url = dto.Url;
        bool b = await _unitOfWork.UpdateAsync(model);
        return b;
    }

    public async Task<long> AddAsync(MenuDto dto)
    {
        var model = new MMenu()
        {
            Sort = dto.Sort,
            Name = dto.Name,
            Url = dto.Url,
            OpenStyle = dto.OpenStyle,
            ParentId = dto.ParentId,
            Icon = dto.Icon,
            Type = dto.Type,
            CreateTime = DateTime.Now,
            IsDeleted = false
        };
        await _unitOfWork.AddAsync(model);
        return model.Id;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var model = await _menuRepository.FindAsync(x => x.Id == id);
        if (model is null) return true;
        bool b = await _unitOfWork.DeleteAsync(model);
        return b;
    }
}