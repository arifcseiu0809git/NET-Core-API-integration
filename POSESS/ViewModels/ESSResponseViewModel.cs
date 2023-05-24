using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSESS.ViewModels
{
    public class ESSResponseViewModel
    {
        public ESSResponseViewModel()
        {
            this.success = false;
            this.requestTime = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm:ss");            
        }
        public bool success { get; set; }
        public string requestTime { get; set; }
        public string responseTime { get; set; }
        public string requestURL { get; set; }
        public dynamic data { get; set; }
        public dynamic errorData { get; set; }
        public List<string> message { get; set; }
    }
}