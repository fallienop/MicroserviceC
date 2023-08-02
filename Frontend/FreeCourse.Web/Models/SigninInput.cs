using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SigninInput
    {
        [Required]
        [Display(Name ="Your Email")]
        public string Username { get; set; }
        

        [Required]
        [Display(Name ="Your Password")]
        public string Password { get; set; }
        [Display(Name ="Remember me")]
        public bool IsRemember { get; set; }
    }
}
