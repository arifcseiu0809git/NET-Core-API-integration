using System;
using System.Net;
using System.Web;

namespace DMS.Common.Helper
{
    public class AuthorizationHelper
    {
        public decimal GetDistributorId()
        {
            try
            {
                var DistributorSession = HttpContext.Current.Session["LoggedDistributorID"]?.ToString();
                if (!string.IsNullOrEmpty(DistributorSession))
                {
                    decimal DistributorId = Convert.ToDecimal(DistributorSession);

                    return DistributorId;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public string GetDistributorCode()
        {
            string DistributorCode = string.Empty;
            try
            {
                string DistributorCodeSession = HttpContext.Current.Session["LoggedDistributorCode"]?.ToString();
                if (!string.IsNullOrEmpty(DistributorCodeSession))
                {
                    DistributorCode = DistributorCodeSession;
                }
                return DistributorCode;
            }
            catch (Exception ex)
            {
                return DistributorCode;
            }

        }
        public decimal GetUserId()
        {
            try
            {
                var UserSession = HttpContext.Current.Session["LoggedUserID"]?.ToString();
                if (!string.IsNullOrEmpty(UserSession))
                {
                    decimal UserId = Convert.ToDecimal(UserSession);

                    return UserId;
                }
                return 11;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public string GetLoggedInUserName()
        {
            try
            {
                var UserSession = HttpContext.Current.Session["LoggedUserLoginName"]?.ToString();
                if (!string.IsNullOrEmpty(UserSession))
                {

                    return UserSession.Trim().ToString(); ;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }

        }
        public string GetUserHostAddress()
        {
            try
            {
                var HostAddress = HttpContext.Current?.Request?.UserHostAddress ?? "" + "-" + Dns.GetHostEntry(HttpContext.Current?.Request?.UserHostAddress ?? "")?.HostName?.ToString() ?? "";
                if (!string.IsNullOrEmpty(HostAddress))
                {

                    return HostAddress;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }

        }
        public string GetLoggedDDMSISDN()
        {
            try
            {
                var UserSession = HttpContext.Current.Session["LoggedDDMSNO"]?.ToString();
                if (!string.IsNullOrEmpty(UserSession))
                {

                    return UserSession.Trim().ToString(); ;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }

        }

    }

    //public class CustomAuthorizeAttribute : AuthorizeAttribute
    //{
    //    private UserRoleRepository repo = new UserRoleRepository(new Entities());

    //    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    //    {
    //        if (filterContext.HttpContext.Session != null)
    //        {
    //            var userId = filterContext.HttpContext.Session["UserId"];


    //            if (userId == null || userId.ToString() == "")
    //            {
    //                //if not logged in
    //                filterContext.Result = new RedirectResult("~/Account/login");
    //            }
    //            else
    //            {
    //                //check the roles
    //                var roles = this.Roles.ToString();

    //                if (!roles.IsNullOrWhiteSpace())
    //                {

    //                    var isAuthenticated = false;

    //                    foreach (var role in roles.Split(','))
    //                    {
    //                        if (repo.IsInRole(Convert.ToDecimal(userId), role.ToString()))
    //                        {
    //                            isAuthenticated = true;
    //                        }
    //                    }

    //                    if (!isAuthenticated)
    //                    {
    //                        filterContext.Result = new RedirectResult("~/Account/UnAuthorized");
    //                    }

    //                }
    //            }
    //        }
    //        else
    //        {
    //            filterContext.Result = new RedirectResult("~/Account/UnAuthorized");
    //        }
    //    }
    //}
}
