using SocialMedia.Core.Entity.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Specification
{
    public class UserWithAllPostsSpec : BaseSpecification<Post>
    {
        public UserWithAllPostsSpec(string id):base(
            p=>p.UserId == id
            )
        {
            Includes.Add(p => p.Comments!); 
        }
    }
}
