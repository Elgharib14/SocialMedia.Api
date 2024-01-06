using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Identity;
using SocialMedia.Reposatory.AppContext;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AppDbcontext dbcontext;
        private readonly UserManager<AppUser> userManager;

        public ImageController(AppDbcontext dbcontext, UserManager<AppUser> userManager)
        {
            this.dbcontext = dbcontext;
            this.userManager = userManager;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetImageUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Unauthorized();

           var data = await dbcontext.Set<Image>().Where(p => p.UserId == user.Id).ToListAsync();

            return Ok(data);
        }
    }
}
