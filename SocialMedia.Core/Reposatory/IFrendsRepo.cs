using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Reposatory
{
    public interface IFrendsRepo
    {
        public Task<Frinds> SendfrindRequest(Frinds  frinds);
        public Frinds CanslefrindRequest(Frinds frinds);
        public Frinds GetFriendRequestById(int Id);
        //public Frinds GetFriendRecevedById(int Id, string receiverId);
        public bool ExistsFriendRequest(string senderId, string receiverId);
        public Task<IEnumerable<Frinds>> GetRecivedFrinds(string id);

        public Frinds UpdateFriendRequest(Frinds frinds);

        public Task<IEnumerable<Frinds>> GetUserFrinds(string userId);
        
    }
}
