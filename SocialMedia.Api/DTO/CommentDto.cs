namespace SocialMedia.Api.DTO
{
    public class CommentDto
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public IFormFile? PhotoComment { get; set; }
    }
}