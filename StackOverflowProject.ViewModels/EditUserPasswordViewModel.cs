﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
   public  class EditUserPasswordViewModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [RegularExpression("^\\S+@\\S+\\.\\S+$")]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
