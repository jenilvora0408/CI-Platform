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
        public bool ChangePassword(int UserID, PasswordReset rpm);
        public User findUser(string email);
        public User findUser(int? id);
        public PasswordReset findUserByToken(string token);
        public void updatePassword(User user);
        public void addUser(User user);
        public void addResetPasswordToken(PasswordReset obj);
        public void removeResetPasswordToekn(PasswordReset obj);

    }
}
