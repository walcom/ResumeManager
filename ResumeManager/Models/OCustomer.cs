using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Models
{
    public class OCustomer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Customer Name")]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Age")]
        [Column(TypeName = "int")]
        public int Age { get; set; }

    }
}
