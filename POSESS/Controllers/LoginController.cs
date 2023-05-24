using DMS.Common.Helper;
using DMS.Services.ESSServices;
using POSESS.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Policy;
using System.Web;
using System.Web.Http;


namespace WebAPIWT.Controllers
{
    public class LoginController : ApiController
    {
        //private readonly IESSService _essService;
        ESSResponseViewModel model = new ESSResponseViewModel();
        public LoginController()
        {
           
        }

        public IHttpActionResult Post([FromBody] LoginRequest login)
        {
            IESSService _essService = new ESSService();
            var url = HttpContext.Current.Request.Url;
            var loginResponse = new LoginResponse { };
            LoginRequest loginrequest = new LoginRequest { };
            loginrequest.Username = login.Username.ToLower();
            loginrequest.Password = login.Password;

            if (!string.IsNullOrEmpty(loginrequest.Username) && !string.IsNullOrEmpty(loginrequest.Password))
            {
                (bool Success, string Message) loginResult = _essService.GetUserInformation(loginrequest);
                // if credentials are valid
                if (loginResult.Success)
                {
                    string token = createToken(loginrequest.Username);
                    loginResponse.Token = token;
                    //return the token
                    model.success = true;
                    model.requestURL = url.ToString();
                    model.data = loginResponse;
                    model.responseTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                    model.message = new List<string> { "Successfully login" };
                }
                else
                {
                    model.responseTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                    model.requestURL = url.ToString();
                    model.data = "";
                    model.message = new List<string> { "Login Failed." };
                    //return response;
                }
            }
            else
            {
                model.responseTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                model.requestURL = url.ToString();
                model.data = "";
                model.message = new List<string> { "Username & Password Requierd." };
            }

            return Ok<ESSResponseViewModel>(model);
        }
        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddMinutes(10);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            string sec = ConfigurationHelper.GetJwtTokenSecurityKey();
            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


            //create the jwt
            var token =
                (JwtSecurityToken)
                    tokenHandler.CreateJwtSecurityToken(issuer: ConfigurationHelper.GetJwtValidIssuer(), audience: ConfigurationHelper.GetJwtValidAudience(),
                        subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);


            return tokenString;
        }

    }
}