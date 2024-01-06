using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.DTO;
using SocialMedia.Core.Entity.Post;
using SocialMedia.Reposatory.AppContext;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly AppDbcontext context;

        public LikeController(AppDbcontext context)
        {
            this.context = context;
        }

        //[HttpPost]
        //public async Task<ActionResult<bool>> AddLike(LikeDto model)
        //{
        //    var data = new Like()
        //    {
        //        PostId = model.PostId,
        //    };
        //    await context.like.AddAsync(data);
        //    context.SaveChanges();
        //    return Ok();
        //}
    }
}
