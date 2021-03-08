using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Models {
    public class Product {

        public Product() { //default constructor
        }

        public int Id { get; set; }

        [StringLength(30), Required] //Attributes
        public string PartNbr { get; set; }

        [StringLength(30), Required] //Attributes
        public string Description { get; set; }

        [Column(TypeName = "decimal(11,2)")] //Attribute
        public decimal Price { get; set; }

        [StringLength(30), Required] // Attribute
        public string Unit { get; set; }

        [StringLength(255)] //Attribute
        public string PhotoPath { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }


    }
}









