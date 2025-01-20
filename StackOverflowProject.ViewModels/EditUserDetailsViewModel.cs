using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
   public class EditUserDetailsViewModel
    {
        public int UserID { get; set; }
        [Required]
        [RegularExpression("^\\S+@\\S+\\.\\S+$")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression("^[A-Z][a-zA-Z]*$")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^\\+?[1-9][0-9]{7,14}$")]
        public string Mobile { get; set; }
    }
}
