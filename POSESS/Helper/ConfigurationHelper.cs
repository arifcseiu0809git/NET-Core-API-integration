using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Common.Helper
{
    public class ConfigurationHelper
    {
        public static string GetAdDomainName()
        {
            return ConfigurationManager.AppSettings["ADDomainName"].ToString();
        }
        public static string GetSPConnectionString()
        {
            string SPConnectionStr = string.Empty;
            SPConnectionStr = ConfigurationManager.AppSettings["PosSPConnectionString"].ToString();
            return SPConnectionStr;
        }
        public static string GetJwtTokenSecurityKey()
        {
            string JwtTokenSecurityKey = string.Empty;
            JwtTokenSecurityKey = ConfigurationManager.AppSettings["JwtTokenSecurityKey"].ToString();
            return JwtTokenSecurityKey;
        }
        public static string GetJwtValidAudience()
        {
            string JwtValidation = string.Empty;
            JwtValidation = ConfigurationManager.AppSettings["JwtValidAudience"].ToString();
            return JwtValidation;
        }
        public static string GetJwtValidIssuer()
        {
            string JwtValidIssuer = string.Empty;
            JwtValidIssuer = ConfigurationManager.AppSettings["JwtValidIssuer"].ToString();
            return JwtValidIssuer;
        }       
    }
}
