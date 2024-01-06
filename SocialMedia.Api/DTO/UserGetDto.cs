using SocialMedia.Core.Entity;
using SocialMedia.Core.Entity.Post;

namespace SocialMedia.Api.DTO
{
    public class UserGetDto
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string userImage { get; set; }
        public ICollection<Post> posts { get; set; } 
        public ICollection<Image> Images { get; set; } 
        public ICollection<Frinds> frinds { get; set; }
    }
}
