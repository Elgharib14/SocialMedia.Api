using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entity
{
    public class Image : BaseEntity
    {
       
        public string UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}
