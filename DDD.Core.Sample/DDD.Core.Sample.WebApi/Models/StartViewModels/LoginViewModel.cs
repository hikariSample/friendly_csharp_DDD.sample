using System.ComponentModel.DataAnnotations;

namespace DDD.Core.Sample.WebApi.Models.StartViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号必填")]
        public string PhoneMob { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码必填")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度应为{2}~{1}个字符")]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [RegularExpression("^.{4}|.{0}$", ErrorMessage = "验证码长度应为4个字符")]
        public string VerifyCode { get; set; }
    }
}