using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public VolunteeringTimesheet getTimesheet(long UserId);

        public void addTimesheet(VolunteeringTimesheet volunteeringTimesheet, long UserId);

        public void deleteTimesheet(long? timesheetId);

        public void updateTimesheet(VolunteeringTimesheet volunteeringTimesheet);

        public List<SelectListItem> GetCountryList();

        public List<SelectListItem> GetSelectedSkills(int userId);

        public List<SelectListItem> GetNotSelectedSkills(int userId);

        List<SelectListItem> GetMissionTitlesByUserIdAndType(long userId, string type);

    }
}
