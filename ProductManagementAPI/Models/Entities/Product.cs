using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage ="ProductName is required")]
        [MinLength(0),MaxLength(50)]
        public string Name{ get; set; }

        [StringLength(250)]
        public string Description{ get; set; }  
        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]
        public int Stock{ get; set; }

        [Required]
        [Column(TypeName="Numeric(8)")]
        public decimal Price{ get; set; }
    }
}