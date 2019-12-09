using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DDD.Core.Sample.WebApi.Models.StartViewModels;
using DDD.Core.Sample.WebApi.ResultModels;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Core.Sample.WebApi.Controllers
{
    /// <summary>
    /// 启动控制器
    /// </summary>
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiController]
    public class StartController : ControllerBase
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/Start/Login
        [HttpPost, Route("Login"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [AllowAnonymous]
        public async Task<object> PostLogin([FromBody]LoginViewModel model)
        {
            MessageBase result = new MessageBase();
            // discover endpoints from metadata
            var client = new HttpClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "https://demo.identityserver.io/connect/token",

                ClientId = "pwd_client",
                ClientSecret = "pwd_secret",
                Scope = "api1 offline_access",

                UserName = "bob",
                Password = "123"
            });
            var json = tokenResponse.Json;
            ////序列化返回的对象
            //var response = new MemberLoginResultModel
            //{
            //    AccessToken = json["access_token"].ToString(),
            //    RefreshToken = json["refresh_token"].ToString(),
            //    ExpiresIn = json["expires_in"].ToInt32()
            //};
            //result.Data = response;
            return result;
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken">刷新token</param>
        /// <returns></returns>
        // GET api/Start/RefreshToken?refreshToken=111
        [HttpGet, Route("RefreshToken"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [AllowAnonymous]
        public async Task<object> RefreshToken(string refreshToken)
        {
            MessageBase result = new MessageBase();
            var client = new HttpClient();
            var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = "https://demo.identityserver.io/connect/token",

                ClientId = "pwd_client",
                ClientSecret = "pwd_secret",

                RefreshToken = refreshToken
            });

            var json = tokenResponse.Json;


            ////序列化返回的对象
            //var response = new MemberLoginResultModel
            //{
            //    AccessToken = json["access_token"].ToString(),
            //    RefreshToken = json["refresh_token"].ToString(),
            //    ExpiresIn = json["expires_in"].ToInt32()
            //};
            //result.Data = response;


            return result;
        }

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <param name="phoneMob">手机号</param>
        /// <returns></returns>
        // GET api/Start/VerifyCode?phoneMob=111
        [HttpGet, Route("VerifyCode"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
        [AllowAnonymous]
        public async Task<object> GetVerifyCode(string phoneMob)
        {
            MessageBase result = new MessageBase();
            string verifyCode = new System.Random().RandomNumber(4);
            //await this._verifyCodeService.Value.AddCodeAsync(phoneMob, verifyCode);
            //var b = YzmHelper.CreateImage(verifyCode);
            //result.Data = b;
            return result;
        }
    }
}