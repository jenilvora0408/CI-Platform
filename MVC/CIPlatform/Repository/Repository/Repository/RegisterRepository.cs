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
            //try
            //{
            User user = _ciPlatformContext.Users.Where(x => x.Email == Email).FirstOrDefault();
            if (user == null)
            {
                //var msg = " Invalid Email";
                return -1;
            }
            else
            {

                return user.UserId;
            }


            //}
            //catch (Exception ex)
            //{
            //    _Message += ex.Message;
            //    return -1;
            //}
        }
        public Boolean ChangePassword(int UserID, NewPassword rpm)
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
