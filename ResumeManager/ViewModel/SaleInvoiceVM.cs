using ResumeManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.ViewModel
{
    public class SaleInvoiceVM
    {
        [Key]
        public int SalesInvoiceID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Invoice Code")]
        public string SalesInvoiceCode { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime SalesInvoiceDate { get; set; }

        [DisplayName("Invoice Date/Time")]
        public string SalesInvoiceDateString { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "This field is required!")]
        [DisplayName("Customer")]
        public int CustomerID { get; set; }

        public List<OCustomer> Customers { get; set; }



        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        public decimal VatAmount { get; set; }


        public virtual List<SaleInvoiceDetailsVM> SaleInvoiceDetails { get; set; } = new List<SaleInvoiceDetailsVM>();

    }
}
