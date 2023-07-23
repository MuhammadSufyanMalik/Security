using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Security
{
    public class JwtHelper : ITokenHelper
    {
        public readonly IConfiguration Configuration;

        private TokenOptions _tokenOptions;

        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        //public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        //{
        //    _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        //    var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        //    var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        //    var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        //    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var token = jwtSecurityTokenHandler.WriteToken(jwt);

        //    return new AccessToken
        //    {
        //        Token = token,
        //        Expiration = _accessTokenExpiration
        //    };

        //}
        public AccessToken CreateToken(string userId, string userName, string email, string fullName, string userTypeId, string refId, List<string> roles)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            JwtSecurityToken token = CreateJwtSecurityToken(signingCredentials, userId, userName, email, fullName, userTypeId, refId, roles);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token2 = jwtSecurityTokenHandler.WriteToken(token);
            return new AccessToken
            {
                Token = token2,
                Expiration = _accessTokenExpiration
            };
        }

        //public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
        //    SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        //{
        //    var jwt = new JwtSecurityToken(
        //        issuer: tokenOptions.Issuer,
        //        audience: tokenOptions.Audience,
        //        expires: _accessTokenExpiration,
        //        notBefore: DateTime.Now,
        //        claims: SetClaims(user, operationClaims),
        //        signingCredentials: signingCredentials
        //    );
        //    return jwt;
        //}


        private JwtSecurityToken CreateJwtSecurityToken(SigningCredentials signingCredentials, string userId, string userName, string email, string fullName, string userTypeId, string refId, List<string> roller)
        {
            return new JwtSecurityToken(_tokenOptions.Issuer, _tokenOptions.Audience, expires: _accessTokenExpiration, notBefore: DateTime.Now, claims: SetClaims(userId, userName, email, fullName, userTypeId, refId, roller), signingCredentials: signingCredentials);
        }
        private IEnumerable<Claim> SetClaims(string userId, string userName, string email, string adSoyad, string userTypeId, string refId, List<string> roles)
        {
            List<Claim> list = new List<Claim>();
            list.AddRefId(refId);
            list.AddUserName(userName);
            list.AddEmail(email.ToString());
            list.AddName(adSoyad);
            list.AddNameIdentifier(userId);
            list.AddRoles(roles.ToArray());
            list.AddUserType(userTypeId);
            return list;
        }

        //private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        //{
        //    var claims = new List<Claim>();
        //    claims.AddNameIdentifier(user.Id.ToString());
        //    claims.AddEmail(user.Email);
        //    claims.AddName($"{user.FirstName} {user.LastName}");
        //    claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

        //    return claims;
        //}
    }
}
