using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class UserSearchResultVM
    {
        public UserSearchResultVM()
        {
            ResultList = new List<UserSearchVM>();
        }

        public List<UserSearchVM> ResultList { get; set; }

        public int ResultCount { get; set; }

        public static UserSearchResultVM ToVM(List<User> userList)
        {
            var vm = new UserSearchResultVM();
            foreach (var user in userList)
            {
                vm.ResultList.Add(new UserSearchVM
                {
                    Id = user.Id,
                    Name = user.Nickname
                });
            }
            vm.ResultCount = vm.ResultList.Count;

            return vm;
        }
    }

    public class UserSearchVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
    }
}