namespace DDD.Core.Sample.Application.DTO;

/// <summary>
/// 菜单数据传输对象
/// </summary>
public class MenuDto
{
    public long Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// url
    /// </summary>
    public string Url { get; set; }
    /// <summary>
    /// 打开方式
    /// </summary>
    public int OpenStyle { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }
    /// <summary>
    /// 父id
    /// </summary>
    public long ParentId { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public int Type { get; set; }
    /// <summary>
    /// 排序，越小越靠前
    /// </summary>
    public int Sort { get; set; }
}