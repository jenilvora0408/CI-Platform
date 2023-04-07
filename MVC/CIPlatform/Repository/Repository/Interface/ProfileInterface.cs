using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface ProfileInterface
    {
        public bool changePassword(editProfile edit, string email);

        public User SaveUserDetail(editProfile userEditProfile, string email);

        public editProfile PutUserDetails(editProfile model, string email);

        public bool ChangeUserProfileImage(string userImgPath, long userId);
    }
}
