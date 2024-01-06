using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.Core.Entity.Post
{
    public class Post : BaseEntity
    {
        public string UserId { get; set; }
        public string? Photo { get; set; }
        public string? Content { get; set; }
        public DateTime TimeAdd { get; set; } = DateTime.Now;
        public ICollection<Like>? likes { get; set; } = new HashSet<Like>();
        public ICollection<Comment>? Comments { get; set; } = new HashSet<Comment>();

    }
}
