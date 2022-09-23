using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DDD.Core.Sample.Domain.Entity;

public class MStudent : IAggregateRoot
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required(AllowEmptyStrings = true)]
    public long Id { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name", TypeName = "varchar(20)")]
    [StringLength(20)]
    [Required(AllowEmptyStrings = true)]
    [Comment("姓名")]
    public string Name { get; set; }
    /// <summary>
    /// 老师id
    /// </summary>
    [Column("teacher_id", TypeName = "bigint")]
    [Required(AllowEmptyStrings = true)]
    [Comment("老师id")]
    public long TeacherId { get; set; }
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

