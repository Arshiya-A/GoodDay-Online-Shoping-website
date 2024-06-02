using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "test")]
        public string Name { get; set; }


        public string Family { get; set; }

        [Required(ErrorMessage = "test")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "test")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}