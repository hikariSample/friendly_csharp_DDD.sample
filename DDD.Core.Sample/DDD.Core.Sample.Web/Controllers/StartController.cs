using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DDD.Core.Sample.Web.Models.StartViewModels;
using DDD.Core.Sample.Web.ResultModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Core.Sample.Web.Controllers
{
    public class StartController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl">返回url</param>
        /// <returns></returns>
        // GET: Start/Login?returnUrl=11111111
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(Login));
        }
        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl">返回地址</param>
        /// <returns></returns>
        // POST: PlatformMember/Login?returnUrl=1111111111
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginViewModel model, string returnUrl = null)
        {
            MessageBase result = new MessageBase();
            ViewData["ReturnUrl"] = returnUrl ?? Url.Action("Index", "Home");

            if (ModelState.IsValid)
            {
               // LoginAccountDto dto = await _loginAccountService.Value.FindAsync(model.UserName, model.Password);

                if (model.UserName=="admin" && model.Password=="admin")
                {
                    var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,"1"),
                    }, CookieAuthenticationDefaults.AuthenticationScheme));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(180))
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Password), "账号或密码不正确");
                    return View(nameof(Login), model);
                }

            }
            return View(nameof(Login), model);
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        // GET: Start/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 进入拒绝
        /// </summary>
        /// <param name="returnUrl">返回url</param>
        /// <returns></returns>
        // GET: Start/AccessDenied?returnUrl=1111111111
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        ///// <summary>
        ///// 验证码
        ///// </summary>
        ///// <returns></returns>
        //// GET: Start/VerificationCode
        //[AllowAnonymous, AcceptVerbs("GET")]
        //public ActionResult VerificationCode()
        //{
        //    string verificationCode = new Random().Str(4);
        //    var bytes = YzmHelper.CreateImage(verificationCode);

        //    TempData["VerificationCode"] = verificationCode.ToUpper();
        //    return File(bytes, "image/jpeg");

        //}
    }
}