using System.ComponentModel.DataAnnotations;
using DDD.Core.Sample.WebApi.Filter;

namespace DDD.Core.Sample.WebApi.Models.StartViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名必填")]
        [CustomRemote(controller: "Start", action: "UserNameExist", ErrorMessage = "用户名未注册")]
        [CustomRemote(controller: "Start", action: "UserLimit", ErrorMessage = "该账号被禁用")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码必填")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度应为{2}~{1}个字符")]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码必填")]
        //[RegularExpression("^.{4}|.{0}$", ErrorMessage = "验证码长度应为4个字符")]
        public string VerifyCode { get; set; }
    }
}