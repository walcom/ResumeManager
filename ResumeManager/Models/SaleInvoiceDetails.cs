using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Models
{
    public class SalesInvoiceDetails
    {
        [Key]
        [Column(TypeName = "int")]
        public int RecordID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int SalesInvoiceID { get; set; }

        [Required]
        public Product Product { get; set; }
        //public int ProductID { get; set; }

        [Required]
        public Unit Unit { get; set; }
        //public int UnitID { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }

    }
}
