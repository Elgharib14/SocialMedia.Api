using SocialMedia.Core.Entity.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Specification
{
    public class PostWithCommentspec : BaseSpecification<Post>
    {
        public PostWithCommentspec() 
        {
            Includes.Add(p => p.Comments );
           
           

        }
    }
}
