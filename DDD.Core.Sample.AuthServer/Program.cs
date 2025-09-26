using DDD.Core.Sample.AuthServer.Configuration;
using Duende.IdentityServer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
});

// Add services to the container.
var services = builder.Services;

//RSA：证书长度2048以上，否则抛异常
//配置AccessToken的加密证书
var rsa = new RSACryptoServiceProvider();
//从配置文件获取加密证书
rsa.ImportCspBlob(Convert.FromBase64String(builder.Configuration["SigningCredential"]));
//IdentityServer4授权服务配置
services.AddIdentityServer()
    .AddSigningCredential(new RsaSecurityKey(rsa), IdentityServerConstants.RsaSigningAlgorithm.RS256) //设置加密证书
                                                                                                      //.AddDeveloperSigningCredential()    //测试的时候可使用临时的证书
    .AddInMemoryApiResources(InMemoryConfiguration.ApiResources())
    .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes())
    .AddInMemoryClients(InMemoryConfiguration.Clients())
    //如果是client credentials模式那么就不需要设置验证User了
    .AddResourceOwnerValidator<UserValidator>(); //User验证接口
// https://localhost:5001/.well-known/openid-configuration
services.AddMvc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseIdentityServer();  //配置asp.net core 管道
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
});

app.Run();