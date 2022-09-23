using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DDD.Core.Sample.Domain.Entity;
/// <summary>
/// 老师表
/// </summary>
[Serializable, Table("m_teacher")]
[Comment("老师表")]
public sealed class MTeacher : IAggregateRoot
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
    /// 学生数量
    /// </summary>
    [Column("student_count", TypeName = "int")]
    [Required(AllowEmptyStrings = true)]
    [Comment("学生数量")]
    public int StudentCount { get; set; }
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

