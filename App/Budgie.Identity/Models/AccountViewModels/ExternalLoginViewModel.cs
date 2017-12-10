using System.ComponentModel.DataAnnotations;

namespace IdentityServerWithAspNetIdentity.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
