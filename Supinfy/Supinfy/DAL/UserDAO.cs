using Supinfy.Models;
using Supinfy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace Supinfy.DAL
{
    public static class UserDAO
    {
        public static List<User> GetAll()
        {
            return DataContext.Instance.Users.ToList();
        }

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

        public static bool CheckAuth(LoginVM model)
        {
            var user = DataContext.Instance.Users.FirstOrDefault(u => u.Email == model.Email);
            return user != null && Crypto.VerifyHashedPassword(user.Password, model.Password);
        }

        public static User GetUserFromUsername(string username)
        {
            return DataContext.Instance.Users.FirstOrDefault(u => u.Nickname == username);
        }

        public static string GetUsernameFromMail(string email)
        {
            return DataContext.Instance.Users.Where(u => u.Email == email).Select(u => u.Nickname).FirstOrDefault();
        }
    }
}