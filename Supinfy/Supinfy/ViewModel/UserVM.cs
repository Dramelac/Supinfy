using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class UserVM
    {
        [Required(ErrorMessage = "EMail is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "NickName is required")]
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public static UserVM ModelToVm(User user)
        {
            return user == null
                ? new UserVM()
                : new UserVM
                {
                    NickName = user.Nickname,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
        }
    }
}