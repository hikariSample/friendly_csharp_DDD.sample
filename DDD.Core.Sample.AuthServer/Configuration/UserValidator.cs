using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace DDD.Core.Sample.AuthServer.Configuration
{
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (context.Password == "123")
            {
                //使用subject可用于在资源服务器区分用户身份等等
                //获取：通过User.Claims.Where(l => l.Type == "sub").FirstOrDefault();获取
                context.Result = new GrantValidationResult(subject: context.UserName, authenticationMethod: "custom");
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.FromResult(0);
        }
    }
}