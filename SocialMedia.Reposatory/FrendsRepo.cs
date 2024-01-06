using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Reposatory.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Reposatory
{
    public class FrendsRepo : IFrendsRepo
    {
        private readonly AppDbcontext dbcontext;

        public FrendsRepo(AppDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Frinds> SendfrindRequest(Frinds frinds)
        {
           await dbcontext.frinds.AddAsync(frinds);
             dbcontext.SaveChanges(); 
            return frinds;
        }

        public Frinds CanslefrindRequest(Frinds frinds)
        {
             dbcontext.frinds.Remove(frinds);
             dbcontext.SaveChanges();
            return frinds;
            
        }
        public async Task<IEnumerable<Frinds>> GetRecivedFrinds(string id)
        {
          var data = await dbcontext.frinds.Where(f=>f.UserReciverId == id && f.Status == "pending").ToListAsync();
            return data;
        }
        public Frinds GetFriendRequestById(int Id )
        {
            return dbcontext.frinds.Where(Fr => Fr.Id == Id ).FirstOrDefault();
        }

        public bool ExistsFriendRequest(string senderId, string receiverId)
        {
            // Perform a query to check if a friend request already exists between the sender and receiver
            return dbcontext.frinds
                .Any(fr => fr.UserSenderId == senderId && fr.UserReciverId == receiverId);
        }

        public Frinds UpdateFriendRequest(Frinds frinds)
        {
           var data = dbcontext.frinds.Update(frinds);
            dbcontext.SaveChanges();
            return frinds;
        }

        public async Task<IEnumerable<Frinds>> GetUserFrinds(string userId)
        {
            var data = await dbcontext.frinds.Where(f => (f.UserReciverId == userId) || (f.UserSenderId == userId) && (f.Status == "accepted")).ToListAsync();
            return data;
        }
    }
}
