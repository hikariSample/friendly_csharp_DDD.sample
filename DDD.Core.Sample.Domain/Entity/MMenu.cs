using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDD.Core.Sample.Domain.Entity;

/// <summary>
/// 菜单表
/// </summary>
[Serializable, Table("m_menu")]
[Comment("菜单表")]
public sealed class MMenu : IAggregateRoot
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = true)]
    public long Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Column("name", TypeName = "varchar(20)")]
    [StringLength(20)]
    [Required(AllowEmptyStrings = true)]
    [Comment("名称")]
    public string  Name { get; set; }
    /// <summary>
    /// url
    /// </summary>
    [Column("url", TypeName = "varchar(255)")]
    [StringLength(255)]
    [Required(AllowEmptyStrings = true)]
    [Comment("url")]
    public string Url { get; set; }
    /// <summary>
    /// 打开方式，0内部打开，1外部打开
    /// </summary>
    [Column("open_style", TypeName = "int")]
    [Required(AllowEmptyStrings = true)]
    [Comment("打开方式")]
    public int OpenStyle { get; set; }
    /// <summary>
    /// 类型，0菜单，1按钮，2接口
    /// </summary>
    [Column("type", TypeName = "int")]
    [Required(AllowEmptyStrings = true)]
    [Comment("类型")]
    public int Type { get; set; }
    /// <summary>
    /// 排序，越小越靠前
    /// </summary>
    [Column("sort", TypeName = "int")]
    [Required(AllowEmptyStrings = true)]
    [Comment("排序")]
    public int Sort { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [Column("icon", TypeName = "varchar(20)")]
    [StringLength(20)]
    [Required(AllowEmptyStrings = true)]
    [Comment("图标")]
    public string Icon { get; set; }
    /// <summary>
    /// 父id
    /// </summary>
    [Column("parent_id", TypeName = "bigint")]
    [Required(AllowEmptyStrings = true)]
    [Comment("父id")]
    public long ParentId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    [Required(AllowEmptyStrings = true)]
    [Comment("创建时间")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    [Comment("更新时间")]
    public DateTime? UpdateTime { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    [Column("is_deleted", TypeName = "bit")]
    [Required(AllowEmptyStrings = true)]
    [DefaultValue(0)]
    [Comment("是否删除")]
    public bool IsDeleted { get; set; }
}