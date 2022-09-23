using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Sample.WebApi.Models.MenuViewModels;
/// <summary>
/// 菜单更新视图模型
/// </summary>
public class UpdateMenuViewModel
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "应为{2}到{1}个字符")]
    [Display(Name = "名称")]
    public string Name { get; set; }
    /// <summary>
    /// url
    /// </summary>
    [StringLength(Int32.MaxValue, MinimumLength = 0, ErrorMessage = "应为{2}到{1}个字符")]
    [Display(Name = "url")]
    public string? Url { get; set; }
    /// <summary>
    /// 打开方式
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
    [Range(typeof(int), "0", "1", ErrorMessage = "值超出范围")]
    [Display(Name = "打开方式")]
    public int OpenStyle { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [StringLength(20, MinimumLength = 0, ErrorMessage = "应为{2}到{1}个字符")]
    [Display(Name = "图标")]
    public string? Icon { get; set; }
    /// <summary>
    /// 父id
    /// </summary>
    [Range(typeof(long), "0", "1000000000", ErrorMessage = "值超出范围")]
    [Display(Name = "父id")]
    public long ParentId { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
    [Range(typeof(int), "0", "4", ErrorMessage = "值超出范围")]
    [Display(Name = "类型")]
    public int Type { get; set; }
    /// <summary>
    /// 排序，越小越靠前
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
    [Range(typeof(int), "0", "1000000000", ErrorMessage = "值超出范围")]
    [Display(Name = "排序")]
    public int Sort { get; set; }
}