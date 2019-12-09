using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Sample.Web.Models.StartViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{2}到{1}个字符")]
        [Display(Name = "账户")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "必填")]
        [Display(Name = "密码")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}