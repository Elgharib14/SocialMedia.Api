using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Api.DTO;
using SocialMedia.Api.Hellper;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Entity.Post;
using SocialMedia.Core.Identity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Core.Specification;
using SocialMedia.Reposatory.AppContext;
using SocialMedia.Reposatory.Identitycontext;
using System.Linq;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IFrendsRepo frendsRepo;
        private readonly IGenericRepo<Image> imgRepo;
        private readonly AppDbcontext dbcontext;
        private readonly UserManager<AppUser> userManager;
        private readonly IGenericRepo<Post> postRepo;
        private readonly IGenericRepo<Comment> commentRepo;
        private readonly IMapper mapper;

        public PostController(IFrendsRepo frendsRepo,IGenericRepo<Image> ImgRepo,AppDbcontext dbcontext,UserManager<AppUser> userManager ,IGenericRepo<Post> postRepo, IGenericRepo<Comment> commentRepo, IMapper mapper)
        {
            this.frendsRepo = frendsRepo;
            imgRepo = ImgRepo;
            this.dbcontext = dbcontext;
            this.userManager = userManager;
            this.postRepo = postRepo;
            this.commentRepo = commentRepo;
            this.mapper = mapper;
        }

        [HttpGet("GetUserPost")]
        [Authorize]
        public async Task<ActionResult> GetUserPost()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Unauthorized();
            var spec = new UserWithAllPostsSpec(user.Id);
            var data = await postRepo.GetAllPostsByuserId(spec);
            return Ok(data);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Post>>> GetAllPosta()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Unauthorized();

            var friendIds = await dbcontext.frinds
                               .Where(f => f.UserSenderId == user.Id || f.UserReciverId== user.Id)
                               .Select(f => f.UserReciverId)
                               .ToListAsync();
          
            var posts = await dbcontext.posts
                            .Where(p => friendIds.Contains(p.UserId))
                            .ToListAsync();
            return Ok(posts);
          
        }


        [HttpGet("id")]
        public async Task<ActionResult<PostDtoreturn>> GetById(int id)
        {
           var post = await postRepo.GetById(id);
            var map = mapper.Map<PostDtoreturn>(post);
            return Ok(map);

        }
        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromForm] PostDto post)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) NotFound();
            var map = mapper.Map<Post>(post);
            map.UserId = user.Id;
            map.Photo = AddFile.UploadFile(post.Photo, "PostPhoto");
            var data = await postRepo.Creat(map);
            if(data.Photo != null)
            {
                var newImage = new Image
                {
                    UserId = user.Id,
                    ImageUrl = data.Photo,
                };
                var Img = await imgRepo.Creat(newImage);
            }
           
           
            return Ok(data);
        }
        [HttpPut]
        public async Task<ActionResult<Post>> UpdatePost(int PostId, [FromForm] PostDto post)
        {
            var oldpost = await postRepo.GetById(PostId);   
            var map = mapper.Map(post,oldpost);
            map.Photo = AddFile.UploadFile(post.Photo, "PostPhoto");
            var data = postRepo.Update(map);
            return Ok(data);
        }
        [HttpDelete]
        public async Task<ActionResult<Post>> DeletPost(int postId)
        {
            var post = await postRepo.GetById(postId);
            var data = postRepo.Delete(post);
            if(data.Photo != null)
                data.Photo = AddFile.DeleteFile(data.Photo, "PostPhoto");
            foreach (var item in data.Comments)
            {
                data.Comments.Remove(item);
            }
            return Ok(data);
        }
    }
}
