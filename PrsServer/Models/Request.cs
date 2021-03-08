using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Models {
    public class Request {

        public Request() { //default constructor
        }

        public static string StatusNew = "NEW";
        public static string StatusReview = "REVIEW";
        public static string StatusApproved = "APPROVED";
        public static string StatusRejected = "REJECTED";
        public static string StatusEdit = "EDIT";

        public int Id { get; set; }

        [StringLength(80), Required] //Attributes
        public string Description { get; set; }

        [StringLength(80), Required] //Attribute
        public string Justification { get; set; }

        [StringLength(80)] //Attributes
        public string RejectionReason { get; set; }

        [StringLength(20), Required]  //Attributes
        public string DeliveryMode { get; set; } = "Pickup";

        [StringLength(10), Required]  //Attributes
        public string Status { get; set; } = "NEW";

        [Column(TypeName = "decimal(11,2)")] //Attributes
        public decimal Total { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }


    }
}























