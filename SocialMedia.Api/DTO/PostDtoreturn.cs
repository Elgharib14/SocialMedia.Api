using AutoMapper.Configuration.Annotations;
using SocialMedia.Core.Entity.Post;

namespace SocialMedia.Api.DTO
{
    public class PostDtoreturn
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string? Content { get; set; }
        public DateTime TimeAdd { get; set; }
        public int likes { get; set; } 
       
        public ICollection<CommentDto>? Comments { get; set; }
    }
}
