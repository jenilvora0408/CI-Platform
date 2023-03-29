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
    }
}
