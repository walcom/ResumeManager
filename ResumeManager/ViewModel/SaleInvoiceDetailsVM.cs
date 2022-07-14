using ResumeManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.ViewModel
{
    public class SaleInvoiceDetailsVM
    {
        [Key]
        public int RecordID { get; set; }

        [Required]
        public int SalesInvoiceID { get; set; }

        [Required]
        public int ProductID { get; set; }

        public List<Product> Products { get; set; }


        [Required]
        public int UnitID { get; set; }

        public List<Unit> Units { get; set; }


        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal GrandTotal { get; set; }

    }
}
