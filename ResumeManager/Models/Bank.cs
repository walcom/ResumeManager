using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Models
{
    public class Bank
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Bank Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string BankName { get; set; }
    }
}
