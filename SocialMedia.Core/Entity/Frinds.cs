using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Entity
{
    public class Frinds
    {
        public int Id { get; set; }
        public string UserSenderId { get; set; }
        public string UserReciverId { get; set; }
        public string Status { get; set; }
    }
}
