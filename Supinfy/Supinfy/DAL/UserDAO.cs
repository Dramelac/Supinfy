using Supinfy.Models;
using Supinfy.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using Supinfy.Utils;

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
                    CreationDateTime = DateTime.Now,
                    Role = Role.Standart,
                    Password = Crypto.HashPassword(model.Password),
                    Playlists = new List<Playlist>(),
                    Friends = new List<User>(),
                    FriendRequests = new List<FriendRequest>()
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

        public static List<User> UserSearch(string query, int resultCount = 100)
        {
            var result = new List<User>();

            result.AddRange(DataContext.Instance.Users.Where(u => 
                    u.Nickname.Contains(query) ||
                    u.FirstName.Contains(query) ||
                    u.Email.Contains(query) ||
                    u.LastName.Contains(query))
                .Take(resultCount));

            return result;
        }

        public static bool AddPendingFriend(Guid userId, Guid friendId)
        {
            var user = GetUser(userId);
            var friend = GetUser(friendId);
            if (friend.FriendRequests.Any(r => r.InitiatorId == userId) || user.Friends.Any(f => f.Id == friendId)) return false;
            var checkRequest = user.FriendRequests.FirstOrDefault(r => r.InitiatorId == friendId);
            if (checkRequest != null)
            {
                user.Friends.Add(friend);
                friend.Friends.Add(user);
                DataContext.Instance.FriendRequests.Remove(checkRequest);
            }
            else
            {
                friend.FriendRequests.Add(new FriendRequest
                {
                    Initiator = user,
                    Target = friend
                });
            }
            DataContext.Instance.SaveChanges();
            return true;
        }

        public static void RemoveFriend(Guid userId, Guid friendId)
        {
            var user = GetUser(userId);
            var friend = GetUser(friendId);
            if (user.Friends.Contains(friend))
            {
                user.Friends.Remove(friend);
                friend.Friends.Remove(user);
            }
            else
            {
                var request = user.FriendRequests.FirstOrDefault(r => r.InitiatorId == friendId);
                if (request != null)
                {
                    DataContext.Instance.FriendRequests.Remove(request);
                }
                else
                {
                    request = DataContext.Instance.FriendRequests.FirstOrDefault(r => r.InitiatorId == userId && r.TargetId == friendId);
                    if (request != null)
                    {
                        DataContext.Instance.FriendRequests.Remove(request);
                    }
                }
            }
            DataContext.Instance.SaveChanges();
        }

        public static List<FriendRequest> GetPendingRequest(Guid userId)
        {
            return DataContext.Instance.FriendRequests.Where(r => r.InitiatorId == userId).ToList();
        }
    }
}