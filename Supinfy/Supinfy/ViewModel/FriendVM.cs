using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.DAL;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class FriendVM
    {
        public UserSearchResultVM PendingFriends { get; set; }

        public UserSearchResultVM WaitingFriends { get; set; }

        public UserSearchResultVM Friends { get; set; }

        public static FriendVM ToVM(User user)
        {
            return new FriendVM
            {
                Friends = UserSearchResultVM.ToVM(user.Friends.ToList()),
                PendingFriends = UserSearchResultVM.ToVM(user.FriendRequests.Where(f => f.TargetId == user.Id).Select(f => f.Initiator).ToList()),
                WaitingFriends = UserSearchResultVM.ToVM(UserDAO.GetPendingRequest(user.Id).Select(r => r.Target).ToList())
            };
        }

    }
}