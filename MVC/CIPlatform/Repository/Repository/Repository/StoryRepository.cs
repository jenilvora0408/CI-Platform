using Entities.Data;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
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
        public StoryRepository(CiPlatformContext ciPlatformContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _ciPlatformContext = ciPlatformContext;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
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
    }
}
