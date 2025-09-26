using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DDD.Core.Sample.AuthServer.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("api1", "My API")
            };
        }
        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new[]
            {
                new ApiScope("api1", "My API")
            };
        }
        public static IEnumerable<Client> Clients()
        {
            // client credentials client
            return new List<Client>
            {
                ////Client Credentials模式
                //new Client
                //{
                //    //client_id
                //    ClientId = "credt_client",
                //    AllowedGrantTypes = new string[] { GrantType.ClientCredentials },
                //    //client_secret
                //    ClientSecrets =new [] {new Secret("credt_secret".Sha256())},
                //    //scope
                //    AllowedScopes = new []
                //    {
                //        "api1",
                //        //Client Credentials模式不支持RefreshToken的，因此不需要设置OfflineAccess
                //        //StandardScopes.OfflineAccess.Name,
                //    },
                //},
                //Resource Owner Password模式
                new Client
                {
                    //client_id
                    ClientId = "pwd_client",
                    AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword },
                    //client_secret
                    ClientSecrets =
                    {
                        new Secret("pwd_secret".Sha256())
                    },
                    //scope
                    AllowedScopes =
                    {
                        "api1",
                        //如果想带有RefreshToken，那么必须设置：StandardScopes.OfflineAccess
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = 1000*60*60*15,//3600, //AccessToken的过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                    //AbsoluteRefreshTokenLifetime = 60, //RefreshToken的最大过期时间，就算你使用了TokenUsage.OneTimeOnly模式，更新的RefreshToken最大期限也是为这个属性设置的(就是6月30日就得要过期[根据服务器时间]，你用旧的RefreshToken重新获取了新RefreshToken，新RefreshToken过期时间也是6月30日)， in seconds. Defaults to 2592000 seconds / 30 day
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,   //默认状态，RefreshToken只能使用一次，使用一次之后旧的就不能使用了，只能使用新的RefreshToken
                    //RefreshTokenUsage = TokenUsage.ReUse,   //重复使用RefreshToken，RefreshToken过期了就不能使用了
                    
                }
            };
        }
    }
}