using Entities.Data;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Repository
{
    public class StoryRepository: StoryInterface
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly CiPlatformContext _ciPlatformContext;
        private readonly IWebHostEnvironment _env;
        public StoryRepository(CiPlatformContext ciPlatformContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _env = env;
        }

        public Boolean RecommandtoCoWorker(long fromUserId, int StoryId, long toUserId)
        {
            var shareStory = _ciPlatformContext.StoryInvites.FirstOrDefault(x => x.FromUserId == fromUserId && x.ToUserId == toUserId && x.StoryId == StoryId);

            if (shareStory != null)
            {
                return true;
            }
            else
            {
                StoryInvite storyInvite = new StoryInvite();
                storyInvite.FromUserId = fromUserId;
                storyInvite.StoryId = StoryId;
                storyInvite.ToUserId = toUserId;
                storyInvite.CreatedAt = DateTime.Now;
                _ciPlatformContext.StoryInvites.Add(storyInvite);
                _ciPlatformContext.SaveChanges();
                return false;
            }
        }

        public void addNotificationForRecommendStory(long StoryId, long userId, string title, string usernameFrom)
        {
            Notification notification = new Notification();
            notification.NotificationMessage = "Your friend " + usernameFrom + " has invited you to join mission - " + title;
            notification.UserId = userId;
            notification.StoryId = StoryId;
            notification.Status = false;
            notification.NotificationType = "Story";
            _ciPlatformContext.Add(notification);
            _ciPlatformContext.SaveChanges();
        }

        public Story FindStoryTitle(long StoryId)
        {
            return _ciPlatformContext.Stories.Where(x => x.StoryId == StoryId).FirstOrDefault();
        }

        public Story GetStoryById(long id)
        {
            return _ciPlatformContext.Stories.FirstOrDefault(x => x.StoryId == id);
        }

        public User GetUserById(long id)
        {
            return _ciPlatformContext.Users.FirstOrDefault(x => x.UserId == id);
        }

        public StoryMedium GetStoryMediaByStoryId(long id)
        {
            return _ciPlatformContext.StoryMedia.FirstOrDefault(x => x.StoryId == id);
        }

        public void UpdateStory(Story story)
        {
            _ciPlatformContext.Stories.Update(story);
            _ciPlatformContext.SaveChanges();
        }

        public List<User> SearchUsers(string name)
        {
            return _ciPlatformContext.Users.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name) || x.Email.Contains(name))
            .Take(10).ToList();
        }


        public Story SubmitStory(long userId, long missionId, string title, string description)
        {
            Story story = _ciPlatformContext.Stories.SingleOrDefault(u => u.UserId == userId && u.MissionId == missionId && u.Title == title);

            if (story == null)
            {
                story = new Story();
                story.Title = title;
                story.MissionId = missionId;
                story.CreatedAt = DateTime.Now;
                story.Status = "Pending";
                story.Description = description;
                story.UserId = userId;
                _ciPlatformContext.Stories.Add(story);
            }
            else
            {
                story.Title = title;
                story.MissionId = missionId;
                story.CreatedAt = DateTime.Now;
                story.Status = "Pending";
                story.Description = description;
                story.UserId = userId;
                _ciPlatformContext.Stories.Update(story);
            }

            _ciPlatformContext.SaveChanges();
            return story;
        }


        public long InsertOrUpdateStory(long missionId, string title, string editorText, long userId)
        {
            var existingStory = _ciPlatformContext.Stories.FirstOrDefault(u => u.UserId == userId && u.MissionId == missionId && u.Title == title);

            if (existingStory == null)
            {
                var newStory = new Story()
                {
                    Title = title,
                    MissionId = missionId,
                    CreatedAt = DateTime.Now,
                    Description = editorText,
                    UserId = userId
                    
                };

                _ciPlatformContext.Stories.Add(newStory);
                _ciPlatformContext.SaveChanges();

                return newStory.StoryId;
            }
            else
            {
                existingStory.Title = title;
                existingStory.MissionId = missionId;
                existingStory.CreatedAt = DateTime.Now;
                existingStory.Description = editorText;
                existingStory.UserId = userId;
                

                _ciPlatformContext.Stories.Update(existingStory);
                _ciPlatformContext.SaveChanges();

                return existingStory.StoryId;
            }
        }

        public IEnumerable<MissionApplication> GetApprovedMissionApplicationsForUser(int userId)
        {
            return _ciPlatformContext.MissionApplications
                .Where(x => x.UserId == userId && x.ApprovalStatus == "Approved")
                .Include(x => x.Mission)
                .AsEnumerable();
        }

        public StoryPage GetStoryListings(int pageNumber)
        {
            var output = new SqlParameter("@TotalCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            StoryPage storyPage = new StoryPage();
            storyPage.Stories = _ciPlatformContext.StoryListings.FromSqlInterpolated($"exec StoryListing @pageNumber={pageNumber},  @TotalCount = {output} out").ToList();
            storyPage.pageSize = 3;
            storyPage.activePage = pageNumber;
            storyPage.pageCount = long.Parse(output.Value.ToString());
            return storyPage;
        }

        public async Task<int> UploadImages(ImageUploadViewModel imageUploadViewModel)
        {
            string imgName = "";
            if (imageUploadViewModel.storyUrl != null)
            {
                StoryMedium storymedia = new StoryMedium();
                storymedia.Path = imageUploadViewModel.storyUrl;
                storymedia.Type = "URL";
                storymedia.StoryId = imageUploadViewModel.StoryId;
                _ciPlatformContext.StoryMedia.Add(storymedia);
                await _ciPlatformContext.SaveChangesAsync();
            }
            foreach (var file in imageUploadViewModel.files)
            {
                var fileName = Path.GetFileName(file.FileName);
                imgName += "/uploads/" + fileName + ",";
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                if (!_ciPlatformContext.StoryMedia.Any(u => u.StoryId == imageUploadViewModel.StoryId))
                {
                    StoryMedium storymedia = new StoryMedium();
                    storymedia.Path = "/uploads/" + fileName;
                    int index = file.ContentType.IndexOf("/");
                    string textAfterSlash = file.ContentType.Substring(index + 1);
                    storymedia.Type = textAfterSlash;
                    storymedia.StoryId = imageUploadViewModel.StoryId;

                    _ciPlatformContext.StoryMedia.Add(storymedia);
                    await _ciPlatformContext.SaveChangesAsync();
                }
                else
                {
                    StoryMedium storymedia = _ciPlatformContext.StoryMedia.Where(u => u.StoryId == imageUploadViewModel.StoryId).First();
                    storymedia.Path = imgName;
                    int index = file.ContentType.IndexOf("/");
                    string textAfterSlash = file.ContentType.Substring(index + 1);
                    storymedia.Type = textAfterSlash;
                    storymedia.StoryId = imageUploadViewModel.StoryId;

                    _ciPlatformContext.StoryMedia.Update(storymedia);
                    await _ciPlatformContext.SaveChangesAsync();
                }
            }
            return (int)imageUploadViewModel.StoryId;
        }

    }
}
