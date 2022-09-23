using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.Core.Sample.Domain;

/// <summary>
/// 聚合根接口，所有的领域模型都应该实现这个接口
/// </summary>
public interface IAggregateRoot
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = true)]
    long Id { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    [Required(AllowEmptyStrings = true)]
    [Comment("创建时间")]
    DateTime CreateTime { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    [Comment("更新时间")]
    DateTime? UpdateTime { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    [Column("is_deleted", TypeName = "bit")]
    [Required(AllowEmptyStrings = true)]
    [DefaultValue(0)]
    [Comment("是否删除")]
    bool IsDeleted { get; set; }
}

