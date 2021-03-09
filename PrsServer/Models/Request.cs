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

        public int Id { get; set; }

        public static string StatusReview = "REVIEW"; // method for review
        public static string StatusApproved = "APPROVED"; // method for approve 
        public static string StatusReject = "REJECTED"; //method for rejected
        
        


        [StringLength(80), Required] //Attributes
        public string Description { get; set; }

        [StringLength(80), Required] //Attribute
        public string Justification { get; set; }

        [StringLength(80)] //Attributes
        public string RejectionReason { get; set; }

        [StringLength(20), Required]  //Attributes
        public string DeliveryMode { get; set; } = "Pickup";

        [StringLength(10), Required]  //Attributes
        public string Status { get; set; } = "New";

        [Column(TypeName = "decimal(11,2)")] //Attributes
        public decimal Total { get; set; } = 0;

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; } //FK to user


    }
}























