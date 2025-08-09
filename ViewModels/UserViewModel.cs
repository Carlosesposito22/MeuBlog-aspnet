using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Informe um nome")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe uma senha")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
