using System.Net.Http;
using System.Web.Http;

namespace WebAPIWT.Controllers
{
    public class LoginResponse
    {
        public LoginResponse()
        {

            this.Token = "";
        }

        public string Token { get; set; }
    }
}