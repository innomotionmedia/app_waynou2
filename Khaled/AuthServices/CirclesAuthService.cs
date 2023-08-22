

using System;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AuthServices;
using System.Linq;
using System.Threading;
using Microsoft.Datasync.Client;

namespace AuthServices
{
	public static  class CirclesAuthService
	{
        public static IPublicClientApplication IdentityClient { get; set; }
        static AuthenticationResult result = null;

        public static async Task<AuthenticationToken> GetAuthenticationToken()
        {
            if (IdentityClient == null)
            {
#if ANDROID
        IdentityClient = PublicClientApplicationBuilder
            .Create(Constants.ApplicationId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "common")
            .WithRedirectUri($"msal{Constants.ApplicationId}://auth")
            .WithB2CAuthority(Constants.AuthoritySignIn)
            .WithParentActivityOrWindow(() => Platform.CurrentActivity)
            .Build();
#elif IOS
            IdentityClient = PublicClientApplicationBuilder
                .Create(Constants.ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
                .WithB2CAuthority(Constants.AuthoritySignIn)
                .WithRedirectUri($"msal{Constants.ClientId}://auth")
                .Build();
#else

                IdentityClient = PublicClientApplicationBuilder
                 .Create(Constants.ClientId)
                 .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                 .WithIosKeychainSecurityGroup("com.microsoft.adalcache")
                 .WithB2CAuthority(Constants.AuthoritySignIn)
                 .WithRedirectUri($"msal{Constants.ClientId}://auth")
                 .Build();
#endif
            }

            var accounts = await IdentityClient.GetAccountsAsync();
            bool tryInteractiveLogin = false;

            if(result != null)
            {
                var exp = result.ExpiresOn;
                if(exp < DateTime.Now)
                {
                    throw new Exception();
                    result = null; 
                }
            }

            if(result == null)
            {
                try
                {
                    result = await IdentityClient
                        .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                        .ExecuteAsync();
                }
                catch (MsalUiRequiredException e)
                {
                    tryInteractiveLogin = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"MSAL Silent Error: {ex.Message}");
                }

                if (tryInteractiveLogin)
                {
                    try
                    {
                        result = await IdentityClient
                            .AcquireTokenInteractive(Constants.Scopes)
                            .ExecuteAsync();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"MSAL Interactive Error: {ex.Message}");
                    }
                }
            }
            

           

            var res = new AuthenticationToken
            {
                DisplayName = result?.Account?.Username ?? "",
                ExpiresOn = result?.ExpiresOn ?? DateTimeOffset.MinValue,
                Token = result?.AccessToken ?? "",
                UserId = result?.Account?.Username ?? ""
            };

            return res;
        }

        public static async Task LogoutAsync()
        {
          //  var accounts = await new IdentityClient.GetAccountsAsync();
          //  foreach (var account in accounts)
          //  {
          //      await IdentityClient.RemoveAsync(account);
          //  }
        }
    }
}

