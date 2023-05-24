using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POSESS.ViewModels
{
    public class ESSViewModel
    {
        [MaxLength(30)]
        [Required]
        public string RECEIVE_CODE { get; set; }
        public DateTime receive_date { get; set; }

        [MaxLength(50)]
        public string invoice_no { get; set; }
        [MaxLength(50)]
        public string lcpo_no { get; set; }
        [MaxLength(100)]
        public string po_no { get; set; }
        [MaxLength(30)]
        public string warehouse_code { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "product_code is required")]
        public string product_code { get; set; }
        
        [MaxLength(1)]
        [Required(ErrorMessage = "commercialized flag is required")]
        [JsonProperty("commercialized_flag")]
        public string COMMERCIALIZEDFLAG { get; set; }
        public int quantity { get; set; }
        [MaxLength(20)]
        public string start_serial { get; set; }
        [MaxLength(20)]
        public string end_serial { get; set; }
        [MaxLength(30)]
        [Required(ErrorMessage = "supplier_code is required")]
        public string supplier_code { get; set; }

        [JsonProperty("supplier_ name")]
        [MaxLength(150)]
        public string SupplierName { get; set; }
        [MaxLength(250)]
        public string supplier_address1 { get; set; }
        [MaxLength(250)]
        public string supplier_address2 { get; set; }
        [MaxLength(30)]
        public string supplier_telephone_no { get; set; }
        [MaxLength(150)]
        public string supplier_contactPerson { get; set; }
        [MaxLength(30)]
        public string supplier_contact_no { get; set; }
        [MaxLength(250)]
        public string supplier_email { get; set; }
    }

    public class ESSTest
    {
        public string RECEIVE_CODE { get; set; }
    }

    public class ESSViewModelNew : ESSViewModel
    {
        public int sn { get; set; }
    }
    public class ESSErrorResponseViewModel : ESSViewModel
    {
        public string error { get; set; }
    }

    public class ESSIvalidData
    {
        public int sn { get; set; }
        public string place { get; set; }
        public string reason { get; set; }
    }

}