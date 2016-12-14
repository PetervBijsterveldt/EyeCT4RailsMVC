using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeCT4RailzMVC.Models
{
    public interface IUserServices
    {
        User CheckForUserId(string userId);
        User CheckForUserNameAndPw(string name, string pw);
        User CheckForUser(User user);
        void AddUser(User user);
        void RemoveUser(User user);
        void EditUser(User user);
        List<User> ListUsers();
    }
}
