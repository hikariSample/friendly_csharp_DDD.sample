using Asp.Versioning;
using DDD.Core.Sample.Application.DTO;
using DDD.Core.Sample.Application.Interfaces;
using DDD.Core.Sample.WebApi.Models.MenuViewModels;
using DDD.Core.Sample.WebApi.ResultModels;
using DDD.Core.Sample.WebApi.ResultModels.MenuResultModels;
using Hikari.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Core.Sample.WebApi.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly Lazy<IMenuService> _menuService;  // 菜单业务接口
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuService"></param>
        public MenuController(Lazy<IMenuService> menuService)
        {
            _menuService = menuService;
        }
        /// <summary>
        /// 获得菜单树
        /// </summary>
        /// <returns></returns>
        // GET api/<Controller>/TreeList
        [HttpGet, Route("TreeList"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [ProducesResponseType(typeof(MenuResultModel), 200)]
        [AllowAnonymous]
        public async Task<object> GetTreeList()
        {
            var dtos = await _menuService.Value.GetListAsync();

            var models = GetTree(dtos);

            return ResultGenerator.GenSuccessResult(models);

        }
        /// <summary>
        /// 获得详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<Controller>/1
        [HttpGet("{id:long}"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [ProducesResponseType(typeof(MenuResultModel), 200)]
        public async Task<object> Get(long id)
        {
            var dto = await _menuService.Value.GetAsync(id);

            var model = new MenuResultModel()
            {
                Sort = dto.Sort,
                ParentId = dto.ParentId,
                Url = dto.Url,
                Icon = dto.Icon,
                Id = dto.Id,
                Type = dto.Type,
                Name = dto.Name,
                OpenStyle = dto.OpenStyle
            };

            return ResultGenerator.GenSuccessResult(model);

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // PATCH api/<Controller>/1
        [HttpPatch("{id:long}"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [ProducesResponseType(typeof(BaseResult), 200)]
        public async Task<object> Patch(long id, [FromBody] UpdateMenuViewModel model)
        {
            var dto = new MenuDto()
            {
                Type = model.Type,
                Sort = model.Sort,
                Icon = model.Icon ?? "",
                Name = model.Name,
                OpenStyle = model.OpenStyle,
                ParentId = model.ParentId,
                Url = model.Url ?? "",
                Id = id
            };
            bool b = await _menuService.Value.UpdateAsync(dto);

            return ResultGenerator.GenSuccessResult(b);

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<Controller>
        [HttpPost, MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [ProducesResponseType(typeof(BaseResult), 200)]
        public async Task<object> Post([FromBody] AddMenuViewModel model)
        {
            var dto = new MenuDto()
            {
                Type = model.Type,
                Sort = model.Sort,
                Icon = model.Icon ?? "",
                Name = model.Name,
                OpenStyle = model.OpenStyle,
                ParentId = model.ParentId,
                Url = model.Url ?? ""
            };
            long id = await _menuService.Value.AddAsync(dto);

            return ResultGenerator.GenSuccessResult(id);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<Controller>/1
        [HttpDelete("{id:long}"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [ProducesResponseType(typeof(BaseResult), 200)]
        public async Task<object> Delete(long id)
        {
            bool b = await _menuService.Value.DeleteAsync(id);

            return ResultGenerator.GenSuccessResult(b);
        }

        /// <summary>
        /// 转化成树结构
        /// </summary>
        /// <param name="menuList">菜单的平级list</param>
        /// <returns></returns>
        private List<MenuResultModel> GetTree(List<MenuDto> menuList)
        {
            var dic = new Dictionary<long, MenuResultModel>(menuList.Count);
            foreach (var chapter in menuList)
            {
                dic.Add(chapter.Id, chapter.ChangeTypeTo<MenuResultModel>());
            }
            foreach (var chapter in dic.Values)
            {
                if (dic.ContainsKey(chapter.ParentId))
                {
                    dic[chapter.ParentId].Children ??= new List<MenuResultModel>();
                    dic[chapter.ParentId].Children?.Add(chapter);
                }
            }
            return dic.Values.Where(t => t.ParentId == 0).ToList();
        }
    }
}
