namespace SocialMedia.Core.Entity.Post
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? PhotoComment { get; set; }
    }
}