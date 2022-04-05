﻿using System.ComponentModel.DataAnnotations;

namespace eShop.Web.CustomerPortal.ViewModels
{
    public class CustomerViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage ="The name must be between 3 and 12 characters")]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerAddress { get; set; }

        [Required]
        public string CustomerCity { get; set; }

        [Required]
        public string CustomerStateProvince { get; set; }

        [Required]
        public string CustomerCountry { get; set; }
    }
}
