using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class FriendVM
    {
        public UserSearchResultVM PendingFriends { get; set; }

        public UserSearchResultVM Friends { get; set; }

        public static FriendVM ToVM(User user)
        {
            return new FriendVM
            {
                Friends = UserSearchResultVM.ToVM(user.Friends.ToList()),
                PendingFriends = UserSearchResultVM.ToVM(user.PendingFriends.ToList())
            };
        }

    }
}