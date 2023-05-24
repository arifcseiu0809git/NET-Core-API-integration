using DMS.Services.ESSServices;
using POSESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebAPIWT.Controllers
{
    [Authorize]
    public class SuplierReceiveController : ApiController
    {
        //private readonly IESSService _essService ;
        public SuplierReceiveController()
        {
            //_essService = new ESSService();
        }

        // POST api/ESS
        public IHttpActionResult Post([FromBody] List<ESSViewModel> requestModel)
        {
            var url = HttpContext.Current.Request.Url;
            ESSResponseViewModel model = new ESSResponseViewModel { };
            IESSService _essService = new ESSService();
            if (ModelState.IsValid)
            {
                (bool Success, string Message, dynamic Data) responseResult = _essService.InsertESS(requestModel);
                if (responseResult.Success)
                {
                    model.success = true;
                    model.requestURL = url.ToString();
                    model.data = "";//responseResult;
                    model.errorData = responseResult.Data;
                    model.responseTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                    model.message = new List<string> { "Successfully data inserted" };
                }
                else
                {
                    model.responseTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                    model.requestURL = url.ToString();
                    model.data = requestModel;
                    model.errorData = responseResult.Data;
                    model.message = new List<string> { responseResult.Message };
                    //return response;
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                model.responseTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");
                model.requestURL = url.ToString();
                //model.data = requestModel;
                model.message = new List<string> { message };
            }

            return Ok<ESSResponseViewModel>(model);
        }
    }
}
