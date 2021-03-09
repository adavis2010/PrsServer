using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PrsServer.Models {
    public class Requestline{

        public Requestline() { //default constructor
        }

        [Required] //attribute
        public int Id { get; set; }

        public int RequestId { get; set; }
        [JsonIgnore]

        public virtual Request Request { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required] // attribute
        public int Quantity { get; set; } = 1;

        









    }




}
