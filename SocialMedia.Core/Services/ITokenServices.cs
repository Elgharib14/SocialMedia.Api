using Microsoft.AspNetCore.Identity;
using SocialMedia.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public interface ITokenServices
    {
        Task<string> CreatUserToken(AppUser user, UserManager<AppUser> userManager); 
    }
}
