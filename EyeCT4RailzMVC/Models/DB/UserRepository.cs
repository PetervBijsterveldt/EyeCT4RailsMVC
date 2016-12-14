using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4RailzMVC.Models
{
    public class UserRepository
    {
        private IUserServices userLogic;

        public UserRepository(IUserServices userLogic)
        {
            this.userLogic = userLogic;
        }

        public User CheckForUserId(string userId)
        {
            return userLogic.CheckForUserId(userId);
        }

        public User CheckForUser(User user)
        {
            return userLogic.CheckForUser(user);
        }

        public User CheckForUserNameAndPw(string name, string pw)
        {
            return userLogic.CheckForUserNameAndPw(name, pw);
        }

        public void AddUser(User user)
        {
            userLogic.AddUser(user);
        }

        public void RemoveUser(User user)
        {
            userLogic.RemoveUser(user);
        }

        public void EditUser(User user)
        {
            userLogic.EditUser(user);
        }

        public List<User> ListUsers()
        {
            return userLogic.ListUsers();
        }
    }
}
