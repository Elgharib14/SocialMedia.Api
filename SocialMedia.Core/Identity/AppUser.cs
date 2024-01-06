using Microsoft.AspNetCore.Identity;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Entity.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Datesign { get; set; } = DateTime.Now;
        public string UserImage { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Post> posts { get; set; } = new HashSet<Post>();
        public ICollection<Image> Images { get; set; } = new HashSet<Image>();
        public ICollection<Frinds> frinds { get; set; } = new HashSet<Frinds>();




    }
}
