namespace AuthServices;

public static class Constants
{
	public static readonly string ClientId = "4dd0f34a-d3ef-45ae-ac81-9dddd0ef9000";
    public static readonly string[] Scopes = { "https://circlesauth.onmicrosoft.com/4dd0f34a-d3ef-45ae-ac81-9dddd0ef9000/Files.Read", "openid", "offline_access" }; 
    // The next code to add B2C	
    public static readonly string TenantName = "circlesauth";
	public static readonly string TenantId = $"{TenantName}.onmicrosoft.com";
	public static readonly string SignInPolicy = "B2C_1_NewUser";
	public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
	public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
	public static readonly string IosKeychainSecurityGroups = "com.microsoft.adalcache";
}