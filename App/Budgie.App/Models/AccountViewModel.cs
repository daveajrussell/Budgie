using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Budgie.App.Models
{
    [Serializable]
    public class AccountViewModel
    {
        [DataMember]
        [EmailAddress]
        [Required]
        public string Username { get; set; }

        [DataMember]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataMember]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmedPassword { get; set; }
    }
}
