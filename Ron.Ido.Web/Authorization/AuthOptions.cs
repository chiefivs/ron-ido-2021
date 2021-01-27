using Microsoft.IdentityModel.Tokens;
using Ron.Ido.BM.Models.Account;
using Ron.Ido.Common.Extensions;
using Ron.Ido.EM.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Ron.Ido.Web.Authorization
{
    public static class AuthOptions
    {
        public const string USERID_CLAIM = "UserIdClaim";
        public const string PERMISSIONS_CLAIM = "PermissionsClaim";
        public const string ISSUER = "ron"; // издатель токена
        public const string AUDIENCE = "http://nic.gov.ru/"; // потребитель токена
        private static string KEY = "F18D2C56F9795A4D29154BD5BEAF3";   // ключ для шифрации
        private static int LIFETIME = 24; // время жизни токена - 24 часа

        public static void SetSettings(AuthOptionSettings settings)
        {
            KEY = settings.SymmetricKey;
            LIFETIME = settings.TokenLifeTimeHours;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        private static Claim CreatePermissionsClaim(IEnumerable<PermissionEnum> permissions)
        {
            return new Claim(PERMISSIONS_CLAIM, permissions.Select(p => ((int)p).ToString()).Join(";"));
        }

        private static Claim CreatePermissionsClaim(params PermissionEnum[] permissions)
        {
            return CreatePermissionsClaim(permissions);
        }

        public static long? ExtractUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return null;

            var claim = principal.Claims.FirstOrDefault(c => c.Type == USERID_CLAIM);
            if (claim == null)
                return null;

            return (long?)claim.Value.Parse(typeof( long ) );
        }

        public static IEnumerable<PermissionEnum> ExtractPermissions(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return new PermissionEnum[] { };

            var claim = principal.Claims.FirstOrDefault(c => c.Type == PERMISSIONS_CLAIM);
            if (claim == null)
                return new PermissionEnum[] { };

            return claim.Value
                .Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => (PermissionEnum)int.Parse(s)).ToArray();
        }

        public static string CreateToken(Identity identity)
        {
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    notBefore: now,
                    claims: CreateClaims(identity),
                    expires: now.Add(TimeSpan.FromHours(LIFETIME)),
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public static IEnumerable<Claim> CreateClaims(Identity identity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, identity.Login),
                new Claim(USERID_CLAIM, identity.Id.ToString()),
                CreatePermissionsClaim(identity.Permissions)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            return claimsIdentity.Claims;
        }

    }
}
