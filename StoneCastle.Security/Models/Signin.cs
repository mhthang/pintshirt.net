﻿using System.ComponentModel.DataAnnotations;

namespace StoneCastle.WebSecurity.Models
{
    public class Signin
    {
        [Required]
        [Display(Name = "Email")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [MaxLength(100)]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }

    }
}
