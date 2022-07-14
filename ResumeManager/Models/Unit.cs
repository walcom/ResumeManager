using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Unit Name")]
        [Column(TypeName = "nvarchar(50)")]
        public string UnitName { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [DisplayName("Unit Name In Arabic")]
        [Column(TypeName = "nvarchar(50)")]
        public string UnitNameAr { get; set; }
    }
}
