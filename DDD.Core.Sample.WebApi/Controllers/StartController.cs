using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DDD.Core.Sample.WebApi.Models.StartViewModels;
using DDD.Core.Sample.WebApi.ResultModels;
using Hikari.Common;
using Hikari.Common.Web.AspNetCore.Swagger.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DDD.Core.Sample.WebApi.Controllers
{
    ///// <summary>
    ///// 启动控制器
    ///// </summary>
    //[ApiVersion("1.0")]
    //[Produces("application/json")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize]
    //[ApiController]
    //public class StartController : ControllerBase
    //{
    //    private readonly IConfiguration _configuration;
    //    //private readonly Lazy<IUserService> _userService;
    //    //private readonly Lazy<IVerifyCodeService> _verifyCodeService;
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="configuration"></param>
    //    /// <param name="userService">用户业务接口</param>
    //    /// <param name="verifyCodeService">验证码业务接口</param>
    //    public StartController(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //        //_userService = userService;
    //        //_verifyCodeService = verifyCodeService;
    //    }

    //    /// <summary>
    //    /// 用户注册
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    // POST api/Start/Register
    //    [HttpPost, Route("Register"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    [AllowAnonymous]
    //    public async Task<object> PostRegister([FromBody] RegisterViewModel model)
    //    {
    //        UserDto dto = new UserDto()
    //        {
    //            UserName = model.UserName,
    //            Password = model.Password,
    //        };

    //        int id = await _userService.Value.RegisterAsync(dto);
    //        result.Data = new
    //        {
    //            Id = id
    //        };

    //        return result;
    //    }
    //    /// <summary>
    //    /// 用户登录
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    // POST api/Start/Login
    //    [HttpPost, Route("Login"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    [ProducesResponseType(typeof(LoginResultModel), 200)]
    //    [Captcha]
    //    [AllowAnonymous]
    //    public async Task<object> PostLogin([FromBody] LoginViewModel model)
    //    {
    //        BaseResult result = new BaseResult();

    //        string xClientId = Request.Headers["X-Client-ID"].ToString();
    //        VerifyCodeDto verifyCodeDto = await _verifyCodeService.Value.FindAsync(xClientId);
    //        if (verifyCodeDto is not null && verifyCodeDto.VerifyCode == model.VerifyCode)
    //        {
    //            bool b = await _verifyCodeService.Value.UpdateAsync(verifyCodeDto);
    //            var dto = await _userService.Value.FindAsync(model.UserName, model.Password);
    //            if (dto is not null)
    //            {
    //                //// discover endpoints from metadata
    //                //var client = new HttpClient();
    //                //// var disco = await client.GetDiscoveryDocumentAsync();
    //                //var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
    //                //{
    //                //    Address = _configuration["OtherConfig:Authority"],
    //                //    Policy = { RequireHttps = false },

    //                //});
    //                //// request token
    //                //var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
    //                //{
    //                //    Address = disco.TokenEndpoint,

    //                //    ClientId = "pwd_client",
    //                //    ClientSecret = "pwd_secret",
    //                //    Password = "123",
    //                //    Scope = "api1 offline_access",
    //                //    UserName = dto.Id.ToString(),
    //                //});


    //                //var json = tokenResponse.Json;

    //                ////序列化返回的对象
    //                //var response = new LoginResultModel
    //                //{
    //                //    Id = dto.Id,
    //                //    AccessToken = json["access_token"].ToString(),
    //                //    RefreshToken = json["refresh_token"].ToString(),
    //                //    ExpiresIn = json["expires_in"].ToInt32(),

    //                //};

    //                //result.Data = response;
    //                var claims = new[]
    //                {
    //                   new Claim(ClaimTypes.Name, dto.Id.ToString()),
    //                    //new Claim("userName", user.userName),
    //                    //new Claim("account", user.account),
    //                    //new Claim("age", user.age.ToString()),
    //                    //new Claim("email", user.email)
    //                };
    //                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
    //                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //                //Claims (Payload)
    //                //Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:
    //                //iss: The issuer of the token，token 是给谁的
    //                //sub: The subject of the token，token 主题
    //                //exp: Expiration Time。 token 过期时间，Unix 时间戳格式
    //                //iat: Issued At。 token 创建时间， Unix 时间戳格式
    //                //jti: JWT ID。针对当前 token 的唯一标识
    //                //除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
    //                var nowTime = DateTime.Now;
    //                var expires = nowTime.AddDays(30);
    //                var token = new JwtSecurityToken(_configuration["JwtSettings:Issuer"],
    //                    _configuration["JwtSettings:Audience"],
    //                    claims,
    //                    nowTime, // 开始有效时间，可以往后设置
    //                    expires,   // 有效期
    //                    creds);
    //                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
    //                //序列化返回的对象
    //                var response = new LoginResultModel
    //                {
    //                    Id = dto.Id,
    //                    AccessToken = accessToken,
    //                    RefreshToken = "",
    //                    ExpiresIn = expires.ToUnixTimeMilliseconds(),

    //                };

    //                result.Data = response;
    //            }
    //            else
    //            {
    //                throw new HttpRequestException("账号或密码错误");
    //            }
    //        }
    //        else
    //        {
    //            throw new HttpRequestException("验证码错误");
    //        }





    //        return result;
    //    }
    //    ///// <summary>
    //    ///// 刷新token
    //    ///// </summary>
    //    ///// <param name="refreshToken">刷新token</param>
    //    ///// <returns></returns>
    //    //// GET api/Start/RefreshToken?refreshToken=111
    //    //[HttpGet, Route("RefreshToken"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    //[ProducesResponseType(typeof(LoginResultModel), 200)]
    //    //[AllowAnonymous]
    //    //public async Task<object> RefreshToken(string refreshToken)
    //    //{
    //    //    MessageBase result = new MessageBase();

    //    //    var client = new HttpClient();
    //    //    // discover endpoints from metadata
    //    //    var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
    //    //    {
    //    //        Address = _configuration["OtherConfig:Authority"],
    //    //        Policy = { RequireHttps = false }
    //    //    });
    //    //    // request token
    //    //    var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest()
    //    //    {
    //    //        Address = disco.TokenEndpoint,

    //    //        ClientId = "pwd_client",
    //    //        ClientSecret = "pwd_secret",
    //    //        Scope = "api1 offline_access",
    //    //        RefreshToken = refreshToken
    //    //    });

    //    //    var json = tokenResponse.Json;


    //    //    //序列化返回的对象
    //    //    var response = new LoginResultModel
    //    //    {
    //    //        AccessToken = json["access_token"].ToString(),
    //    //        RefreshToken = json["refresh_token"].ToString(),
    //    //        ExpiresIn = json["expires_in"].ToInt32()
    //    //    };
    //    //    result.Data = response;

    //    //    return result;
    //    //}
    //    ///// <summary>
    //    ///// 获得验证码
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //// GET api/Start/CaptchaImage
    //    //[HttpGet, Route("CaptchaImage"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    //[ProducesResponseType(typeof(CaptchaResultModel), 200)]
    //    //[Captcha]
    //    //[AllowAnonymous]
    //    //public async Task<object> GetCaptchaImage()
    //    //{
    //    //    string xClientId = Request.Headers["X-Client-ID"].ToString();

    //    //    BaseResult result = new BaseResult();
    //    //    var v = CaptchaHelper.CreateCaptcha();

    //    //    VerifyCodeDto dto = new VerifyCodeDto()
    //    //    {
    //    //        ClientId = xClientId,
    //    //        RandomCode = v.randomCode,
    //    //        VerifyCode = v.value.ToString()
    //    //    };
    //    //    long id = await _verifyCodeService.Value.AddAsync(dto);

    //    //    var cr = new CaptchaResultModel()
    //    //    {
    //    //        //RandomCode = v.randomCode,
    //    //        Pic = v.pic,
    //    //        //Value = v.value
    //    //    };

    //    //    result.Data = cr;

    //    //    return result;
    //    //}
    //    /// <summary>
    //    /// 获得验证码图片
    //    /// </summary>
    //    /// <returns></returns>
    //    // GET api/Start/CaptchaImage
    //    [HttpGet, Route("CaptchaImage"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    [AllowAnonymous]
    //    public async Task<object> GetCaptchaImage()
    //    {
    //        var webImage = new WebImageHelper();
    //        var v = await webImage.GetGenshinImageAsync();

    //        return new FileContentResult(v, "image/png");
    //    }
    //    /// <summary>
    //    /// 获得验证码
    //    /// </summary>
    //    /// <returns></returns>
    //    // GET api/Start/Captcha
    //    [HttpGet, Route("Captcha"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    [ProducesResponseType(typeof(CaptchaResultModel), 200)]
    //    [Captcha]
    //    [AllowAnonymous]
    //    public async Task<object> GetCaptcha(string clientId)
    //    {
    //        var v = new Random().RandomStr(6);

    //        VerifyCodeDto dto = new VerifyCodeDto()
    //        {
    //            ClientId = clientId,
    //            RandomCode = "",
    //            VerifyCode = v
    //        };
    //        long id = await _verifyCodeService.Value.AddAsync(dto);

    //        var cr = new CaptchaResultModel()
    //        {
    //            VerifyCode = v
    //        };

    //        return ResultGenerator.GenSuccessResult(cr);
    //    }
    //    /// <summary>
    //    /// 获得登录状态
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet, Route("LoginStatus"), MapToApiVersion("1.0"), EnableCors("AllowSpecificOrigin")]
    //    [Authorize]
    //    public object GetLoginStatus()
    //    {
    //        return true;
    //    }

    //    #region 模型认证

    //    /// <summary>
    //    /// 用户名存在验证
    //    /// </summary>
    //    /// <param name="userName">用户名</param>
    //    /// <returns>存在为true</returns>
    //    private async Task<object> UserNameExist(string userName)
    //    {
    //        bool b = await _userService.Value.ExistUserNameAsync(userName) > 0;
    //        return new JsonResult(b);
    //    }
    //    /// <summary>
    //    /// 用户名不存在验证
    //    /// </summary>
    //    /// <param name="userName">用户名</param>
    //    /// <returns>不存在为true</returns>
    //    private async Task<object> UserNameNotExist(string userName)
    //    {
    //        bool b = await _userService.Value.ExistUserNameAsync(userName) > 0;
    //        return new JsonResult(!b);
    //    }
    //    /// <summary>
    //    /// 是否禁用
    //    /// </summary>
    //    /// <param name="userName">用户名</param>
    //    /// <returns>启用为ture</returns>
    //    private async Task<object> UserLimit(string userName)
    //    {
    //        UserDto dto = await _userService.Value.FindAsync(userName);
    //        if (dto != null && (dto.Status == 0))
    //        {
    //            return new JsonResult(false);
    //        }
    //        return new JsonResult(true);
    //    }

    //    #endregion
    //}
}
