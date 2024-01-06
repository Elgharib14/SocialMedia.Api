using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Entity.Post;

namespace SocialMedia.Reposatory.AppContext
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
            
        }
        public DbSet<Post> posts { set; get; }
        public DbSet<Comment> comments { set; get; }
        public DbSet<Like> like { set; get; }
        public DbSet<Image> Images { set; get; }
        public DbSet<Frinds> frinds { set; get; }

    }
}
