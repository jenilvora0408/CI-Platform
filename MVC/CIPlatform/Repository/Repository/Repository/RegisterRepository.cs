using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace Repository.Repository.Repository
{
    public class RegisterRepository: RegisterInterface
    {
        private readonly CiPlatformContext _ciPlatformContext;
        public RegisterRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
        }
        public List<User> GetUsersList()
        {
            List<User> lstUsers = _ciPlatformContext.Users.ToList();
            return lstUsers;
        }
        public void InsertUser(User user)
        {
            _ciPlatformContext.Users.Add(user);
            _ciPlatformContext.SaveChanges();
        }
        public Boolean isEmailAvailable(string email)
        {
            return _ciPlatformContext.Users.Any(x => x.Email == email);
        }
        public User isPasswordAvailable(string password, string email)
        {
            return _ciPlatformContext.Users.Where(x => x.Password == password && x.Email == email).FirstOrDefault();
        }

        public long GetUserID(string Email)
        {
            User user = _ciPlatformContext.Users.Where(x => x.Email == Email).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }
            else
            {

                return user.UserId;
            }
        }

        public IEnumerable<User> getUsers()
        {
            var Users = _ciPlatformContext.Users;
            return Users;
        }
        void RegisterInterface.addResetPasswordToken(PasswordReset passwordResetObj)
        {
            bool isAlreadyGenerated = _ciPlatformContext.PasswordResets.Any(u => u.Email.Equals(passwordResetObj.Email));
            if (isAlreadyGenerated)
            {
                _ciPlatformContext.Update(passwordResetObj);

            }
            else
            {
                _ciPlatformContext.Add(passwordResetObj);
            }
            _ciPlatformContext.SaveChanges();
        }

        void RegisterInterface.addUser(User user)
        {
            _ciPlatformContext.Users.Add(user);
            _ciPlatformContext.SaveChanges();
        }

        User RegisterInterface.findUser(string email)
        {
            return _ciPlatformContext.Users.Where(u => u.Email.Equals(email)).First();
        }
        User RegisterInterface.findUser(int? id)
        {
            return _ciPlatformContext.Users.Where(u => u.UserId == id).First();
        }

        PasswordReset RegisterInterface.findUserByToken(string token)
        {
            return _ciPlatformContext.PasswordResets.Where(u => u.Token.Equals(token)).First();
        }

        void RegisterInterface.removeResetPasswordToekn(PasswordReset obj)
        {
            _ciPlatformContext.Remove(obj);
            _ciPlatformContext.SaveChanges();
        }

        void RegisterInterface.updatePassword(User user)
        {
            _ciPlatformContext.Update(user);
            _ciPlatformContext.SaveChanges();
        }

        bool RegisterInterface.ChangePassword(int UserID, PasswordReset rpm)
        {
            try
            {
                User user = _ciPlatformContext.Users.Find(UserID);
                //user.Password = protector.Protect(rpm.Password);
                _ciPlatformContext.Users.Update(user);
                _ciPlatformContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }

}



