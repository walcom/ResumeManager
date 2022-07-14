using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Models
{
    public class SaleInvoice
    {
        [Key]
        public int SalesInvoiceID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("SaleInvoice Code")]
        [Column(TypeName = "nvarchar(50)")]
        public string SalesInvoiceCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime SalesInvoiceDate { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public OCustomer Customer { get; set; }

        [DisplayName("Remarks")]
        [Column(TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }

        [Column(TypeName = "money")]
        public decimal VatAmount { get; set; }


        public virtual List<SalesInvoiceDetails> SaleInvoiceDetails { get; set; } = new List<SalesInvoiceDetails>();

    }
}
