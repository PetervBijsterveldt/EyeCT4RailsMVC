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
        User CheckForUser(User user);
        void AddUser(User user);
        void RemoveUser(string naam);
        void EditUser(User user);
        List<User> ListUsers();
        int UserIDByName(string naam);
    }
}
