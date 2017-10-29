using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Budgie.App.Models
{
    [Serializable]
    public class LoginAuthModel
    {
        [DataMember]
        [EmailAddress]
        [Required]
        public string UserName { get; set; }

        [DataMember]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataMember]
        public bool RememberMe { get; set; }
    }
}
