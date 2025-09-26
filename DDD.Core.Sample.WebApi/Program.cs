using Asp.Versioning.ApiExplorer;
using DDD.Core.Sample.WebApi.ResultModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using NLog.Web;
using System.Net;
using System.Text;
using Hikari.Common.Web.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
//builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
//{
//    config.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
//});
builder.Configuration.AddJsonFile("hosting.json", optional: true, reloadOnChange: true);
builder.WebHost.UseNLog();

// Add services to the container.
var services = builder.Services;

DDD.Core.Sample.BootStrapper.Startup.AddConfigureServices(services, builder.Configuration);

// 添加跨域请求
services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration["OtherConfig:Cors"]!.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .AllowAnyHeader().WithHeaders(HeaderNames.Authorization, HeaderNames.ContentType, HeaderNames.Accept, HeaderNames.Origin)
            .AllowAnyMethod().WithMethods("PATCH").WithMethods("DELETE")
            .AllowCredentials();

    });
});

services.AddAuthorization();
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,  // 是否验证Issuer
            ValidateAudience = true,  // 是否验证Audience
            ValidateLifetime = true,  // 是否验证失效时间
            ClockSkew = TimeSpan.FromSeconds(30),  // 缓冲过期时间
            ValidateIssuerSigningKey = true,  // 是否验证SecurityKey
            ValidAudience = builder.Configuration["JwtSettings:Audience"],  // Audience
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],  // Issuer，这两项和前面签发jwt的设置一致
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))  // 拿到SecurityKey

        };
    });
//.AddIdentityServerAuthentication(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.Authority = Configuration["OtherConfig:Authority"];
//    options.ApiName = "api1";
//    options.ApiSecret = "pwd_secret";
//})
//.AddJwtBearer("Bearer", options =>
//{
//    options.Authority = builder.Configuration["OtherConfig:Authority"];
//    options.RequireHttpsMetadata = false;
//    options.Audience = "api1";
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateAudience = false
//    };
//});

services.Configure<ApiBehaviorOptions>(options =>
{
    //options.SuppressConsumesConstraintForFormFileParameters = true;
    //options.SuppressInferBindingSourcesForParameters = true;
    //options.SuppressModelStateInvalidFilter = true;
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        IDictionary<string, object> errors = new Dictionary<string, object>();
        StringBuilder sb = new StringBuilder();
        // 获取所有错误的Key
        List<string> keys = actionContext.ModelState.Keys.ToList();
        foreach (string key in keys)
        {
            var error = actionContext.ModelState[key]!.Errors.Select(e => e.ErrorMessage).ToList();
            errors.Add(new KeyValuePair<string, object>(key, error));
            foreach (var o in error)
            {
                sb.AppendFormat("{1};", key, o);
            }

        }

        // 设置返回内容
        var result = new BaseResult()
        {
            Success = false,
            Code = HttpStatusCode.BadRequest,
            Message = sb.ToString().TrimEnd(';')  // errors.ToDynamic()
        };
        return new BadRequestObjectResult(result);
    };
});


builder.Services.AddControllers().AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
services.AddOpenApiCustom();

var app = builder.Build();

IApiVersionDescriptionProvider provider = app.Services.GetService<IApiVersionDescriptionProvider>()!;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseExceptionHandler(applicationBuilder => applicationBuilder.Run(async context =>
{
    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = feature?.Error;
    BaseResult result = new()
    {
        Success = false,
        Code = HttpStatusCode.InternalServerError,
        Message = exception?.Message ?? "an error occure"
    };
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
}));
//// 在首次运行时，完全按照在上下文类中的DbContext模型来创建数据库
//using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
//{
//    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
//    context.Database.EnsureCreated();
//    //context.Database.MigrateAsync();  // 迁移
//}

DDD.Core.Sample.BootStrapper.Startup.AddConfigure();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
