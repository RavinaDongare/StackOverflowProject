using System.ComponentModel.DataAnnotations;

namespace StackOverflowProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression("^\\S+@\\S+\\.\\S+$")]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }


        [Required]
        public string ConfirmPassword { get; set; }


        [Required]
        //[RegularExpression("^[A-Z][a-zA-Z]*$")]
        public string Name { get; set; }


        [Required]
        [RegularExpression("^\\+?[1-9][0-9]{7,14}$")]
        public string Mobile { get; set; }
    }
}
