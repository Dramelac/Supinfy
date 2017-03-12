using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class ProfileVM
    {
        public Guid Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public string NickName { get; set; }

        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        public string LastName { get; set; }
        
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string VerifyPassword { get; set; }

        public bool IsOwner { get; set; }

        public static ProfileVM ModelToVm(User user)
        {
            return user == null
                ? new ProfileVM()
                : new ProfileVM
                {
                    Id = user.Id,
                    NickName = user.Nickname,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsOwner = false
                };
        }

    }
}