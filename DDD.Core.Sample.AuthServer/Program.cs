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

//RSA��֤�鳤��2048���ϣ��������쳣
//����AccessToken�ļ���֤��
var rsa = new RSACryptoServiceProvider();
//�������ļ���ȡ����֤��
rsa.ImportCspBlob(Convert.FromBase64String(builder.Configuration["SigningCredential"]));
//IdentityServer4��Ȩ��������
services.AddIdentityServer()
    .AddSigningCredential(new RsaSecurityKey(rsa), IdentityServerConstants.RsaSigningAlgorithm.RS256) //���ü���֤��
                                                                                                      //.AddDeveloperSigningCredential()    //���Ե�ʱ���ʹ����ʱ��֤��
    .AddInMemoryApiResources(InMemoryConfiguration.ApiResources())
    .AddInMemoryApiScopes(InMemoryConfiguration.ApiScopes())
    .AddInMemoryClients(InMemoryConfiguration.Clients())
    //�����client credentialsģʽ��ô�Ͳ���Ҫ������֤User��
    .AddResourceOwnerValidator<UserValidator>(); //User��֤�ӿ�
// https://localhost:5001/.well-known/openid-configuration
services.AddMvc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseIdentityServer();  //����asp.net core �ܵ�
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
});

app.Run();