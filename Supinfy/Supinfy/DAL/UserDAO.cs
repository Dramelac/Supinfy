using Supinfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supinfy.DAL
{
    public static class UserDAO
    {
        public static List<User> GetAll()
        {
            return DataContext.Instance.Users.ToList();
        }

        public static void AddUser(User user)
        {
            DataContext.Instance.Users.Add(user);
            DataContext.Instance.SaveChanges();
        }
    }
}