using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Entities.Dtos
{
    public class OrderDto
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name ="City")]
        public string City { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Card Name")]
        public string CardName { get; set; }
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        [Display(Name = "Month")]
        public string ExpirationMonth { get; set; }
        [Display(Name = "Year")]
        public string ExpirationYear { get; set; }
        [Display(Name = "CVV")]
        public string CVV { get; set; }
        public CartDto CartDto { get; set; }
    }
}
