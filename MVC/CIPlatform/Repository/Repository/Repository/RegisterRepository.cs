using Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Repository
{
    public class RegisterRepository
    {
        private readonly CiPlatformContext _ciPlatformContext;
        public RegisterRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
        }
    }
}
