using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Controllers.ControllersEntities;


namespace WebApplication.Controllers
{
    /// <summary>
    /// Контроллер управления учетными записями
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private List<UserInfo> users = new List<UserInfo>
        {
            new UserInfo { Login="detailsadmin", Password="57GH8n7j" }
        };

        #region POST {host}/Login - Авторизация

        [HttpPost("/Login")]
        public IActionResult Login([FromBody] UserInfo userInfo)
        {
            var identity = GetIdentity(userInfo);
            if (identity == null)
            {
                return StatusCode(400);
            }

            var token = GetToken(identity);
            return Json(token);
        }

        #endregion
        
        // todo nika : Здесь должен быть роут "я забыл пароль"
        // todo nika : Здесь должен быть роут для смены пароля


        private ClaimsIdentity GetIdentity(UserInfo userInfo)
        {
            // todo nika : сделать проверку по базе
            var user = users.FirstOrDefault(x => x.Login == userInfo.Login && x.Password == userInfo.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        private string GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.KEY, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
