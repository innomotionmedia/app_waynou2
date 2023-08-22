using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace AuthServices
{
	public class JWTDecoder
	{

		public static JwtSecurityToken GetDecodedToken(string token)
		{
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }

        public static string GetDecodedEmail(string token)         
        {
            var dec = GetDecodedToken(token);
            return dec.Claims.First(claim => claim.Type == "emails").Value;
        }
    }
}

