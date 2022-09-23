using System.ComponentModel.DataAnnotations;
using DDD.Core.Sample.WebApi.Filter;

namespace DDD.Core.Sample.WebApi.Models.StartViewModels
{
    /// <summary>
    /// 注册模型
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用户名必填")]
        [StringLength(11, MinimumLength = 6, ErrorMessage = "用户名长度应为{2}到{1}个字符")]
        [CustomRemote(controller:"Start",action: "UserNameNotExist", ErrorMessage = "已存在的用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码必填")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密码长度应为{2}到{1}个字符")]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "确认密码必填")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "确认密码长度应为{2}~{1}个字符")]
        [Compare("Password", ErrorMessage = "密码不匹配")]
        public string ConfirmPassword { get; set; }
        ///// <summary>
        ///// 短信验证码
        ///// </summary>
        //[Required(AllowEmptyStrings = false, ErrorMessage = "短信验证码必填")]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "短信验证码长度应为{2}个字符")]
        //// [SmsCodeValid(ErrorMessage = "短信验证码不正确或已失效")]
        //public string VerifyCode { get; set; }

        ///// <summary>
        ///// 邀请码(资产单元id)
        ///// </summary>
        //// [RefereeCodeExist(ErrorMessage = "邀请码不存在")]
        //public string RefereeCode { get; set; }
    }
}