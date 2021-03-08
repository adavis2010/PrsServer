using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Models {
    public class User {
        //default constructor
        public User() { }

        [Required]
        public int Id { get; set; } //primary key

        [StringLength(30),Required] //attribute
        public string Username { get; set; }

        [StringLength(30), Required] // attribute
        public string Password { get; set; }

        [StringLength(30), Required]//attribute
        public string Firstname { get; set; }
        
        [StringLength(30), Required]//attribute
        public string Lastname { get; set; }

        [StringLength(12)]//attribute
        public string Phone { get; set; }

        [StringLength(255)]//attribute
        public string Email { get; set; }

        public bool IsReviewer { get; set; }

        public bool IsAdmin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)


















    }


}
