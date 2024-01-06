using SocialMedia.Core.Entity.Post;

namespace SocialMedia.Api.DTO
{
    public class LoginDtoReturn
    {
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string UserImage { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public ICollection<PostDto> posts { get; set; }
    }
}
