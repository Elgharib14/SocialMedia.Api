using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.DTO;
using SocialMedia.Api.Hellper;
using SocialMedia.Core.Entity.Post;
using SocialMedia.Core.Identity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Reposatory.AppContext;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        
        private readonly IGenericRepo<Comment> commentRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public CommentController(IGenericRepo<Comment> commentRepo, UserManager<AppUser> userManager , IMapper mapper)
        {
            this.commentRepo = commentRepo;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public  async Task<ActionResult<Comment>> AddComment([FromForm]CommentDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var comment = mapper.Map<Comment>(dto);
            comment.UserId = user.Id;
            comment.PhotoComment = AddFile.UploadFile(dto.PhotoComment, "CommetPhoto");
           var data =  await commentRepo.Creat(comment);
            return Ok(data);
        }
        [HttpPut]
        public async Task<ActionResult<Comment>> EditComment(int commentId ,[FromForm]CommentDto dto)
        {
            var comment = await commentRepo.GetById(commentId);
            if (comment == null)
                return BadRequest();
            comment.Title = dto.Title;
            comment.PostId = comment.PostId;
            comment.PhotoComment = AddFile.UploadFile(dto.PhotoComment, "CommetPhoto");
            var data = commentRepo.Update(comment);
            return Ok(data);
        }


        [HttpDelete]
        public async Task<ActionResult<Comment>> DeletComment(int commentId)
        {
            var comment = await commentRepo.GetById(commentId);
            var data =  commentRepo.Delete(comment);
            return Ok(data);    
        }
    }
}
