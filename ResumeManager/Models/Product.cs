using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [MaxLength(12)]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Product Code")]
        [Column(TypeName = "nvarchar(12)")]
        public string ProductCode { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Product Name")]
        [Column(TypeName = "nvarchar(200)")]
        public string ProductName { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Product Generic Name")]
        [Column(TypeName = "nvarchar(200)")]
        public string ProductGenericName { get; set; }

        [Required]
        public Unit Unit { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Price")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Sale Price")]
        [Column(TypeName = "money")]
        public decimal SalePrice { get; set; }


    }
}
