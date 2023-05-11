using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface StoryInterface
    {
        public Boolean RecommandtoCoWorker(long fromUserId, int StoryId, long toUserId);

        Story GetStoryById(long id);

        User GetUserById(long id);

        StoryMedium GetStoryMediaByStoryId(long id);

        void UpdateStory(Story story);

        public List<User> SearchUsers(string name);

        public Story SubmitStory(long userId, long missionId, string title, string description);

        public long InsertOrUpdateStory(long missionId, string title, string editorText, long userId);

        public IEnumerable<MissionApplication> GetApprovedMissionApplicationsForUser(int userId);

        public StoryPage GetStoryListings(int pageNumber);

        public Task<int> UploadImages(ImageUploadViewModel imageUploadViewModel);

        public Story FindStoryTitle(long StoryId);

        public void addNotificationForRecommendStory(long StoryId, long userId, string title, string usernameFrom);
    }
}
