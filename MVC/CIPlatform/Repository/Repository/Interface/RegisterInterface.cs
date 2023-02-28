using Entities.ViewModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface RegisterInterface
    {
        public List<User> GetUsersList();
        public void InsertUser(User user);
        Boolean isEmailAvailable(string email);
        User isPasswordAvailable(string password, string email);
        public long GetUserID(string Email);
        public Boolean ChangePassword(int UserID, NewPassword rpm);

    }
}
