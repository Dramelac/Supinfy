using Supinfy.Models;
using Supinfy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;

namespace Supinfy.DAL
{
    public static class UserDAO
    {
        public static bool AddUser(UserVM model)
        {
            try
            {
                var user = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Nickname = model.NickName,
                    Password = Crypto.HashPassword(model.Password)
                };
                DataContext.Instance.Users.Add(user);
                DataContext.Instance.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public static bool UpdateUser(ProfileVM vm)
        {
            var user = GetUser(vm.Id);
            if (user == null) return false;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            if (!vm.OldPassword.IsNullOrWhiteSpace() && CheckAuth(user.Email, vm.OldPassword) && vm.Password == vm.VerifyPassword)
            {
                user.Password = Crypto.HashPassword(vm.Password);
            }
            user.Email = vm.Email;
            return true;
        }

        public static bool CheckAuth(LoginVM model)
        {
            return CheckAuth(model.Email, model.Password);
        }

        public static bool CheckAuth(string email, string password)
        {
            var user = DataContext.Instance.Users.FirstOrDefault(u => u.Email == email);
            return user != null && Crypto.VerifyHashedPassword(user.Password, password);
        }

        public static User GetUserFromUsername(string username)
        {
            return DataContext.Instance.Users.FirstOrDefault(u => u.Nickname == username);
        }

        public static User GetUserFromMail(string email)
        {
            return DataContext.Instance.Users.FirstOrDefault(u => u.Email == email);
        }

        public static User GetUser(Guid id)
        {
            return DataContext.Instance.Users.Find(id);
        }

        public static string GetUsernameFromMail(string email)
        {
            return DataContext.Instance.Users.Where(u => u.Email == email).Select(u => u.Nickname).FirstOrDefault();
        }

        public static void UpdateUser()
        {
            
        }
    }
}