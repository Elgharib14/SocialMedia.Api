namespace SocialMedia.Api.DTO
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public IFormFile UserImage { get; set; }
        public string Bio { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
