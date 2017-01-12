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

        public void AddUser(User user)
        {
            userLogic.AddUser(user);
        }

        public void RemoveUser(string naam)
        {
            userLogic.RemoveUser(naam);
        }

        public void EditUser(User user)
        {
            userLogic.EditUser(user);
        }

        public List<User> ListUsers()
        {
            return userLogic.ListUsers();
        }

        public int UserIDByName(string naam)
        {
            return userLogic.UserIDByName(naam);
        }
    }
}
