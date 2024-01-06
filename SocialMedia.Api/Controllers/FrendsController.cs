using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.DTO;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Identity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Reposatory.AppContext;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrendsController : ControllerBase
    {
        private readonly IFrendsRepo repo;
        private readonly UserManager<AppUser> userManager;

        public FrendsController(IFrendsRepo repo , UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }

        [HttpPost("SendToFrind")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Frinds>>> SendToFrind(FrindDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var data = new Frinds
            {
                Status = "pending",
                UserReciverId = dto.UserReciverId,
                UserSenderId = user.Id
            };

            bool friendRequestExists = repo.ExistsFriendRequest(data.UserSenderId, data.UserReciverId);
            if (friendRequestExists)
            {
                return BadRequest("A friend request already exists between the sender and receiver.");
            }

            var send = repo.SendfrindRequest(data);
            return Ok(send);
        }


        [HttpDelete("CanselRequest")]
        [Authorize]
        public async Task<ActionResult<Frinds>> CanselRequest(int Id )
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            var data = repo.GetFriendRequestById(Id);
            if(data == null)
                return NotFound();
            var delet = repo.CanslefrindRequest(data);
            return Ok(delet);
        }

        [HttpPost("AccpetRequest")]
        [Authorize]
        public async Task<ActionResult<Frinds>> AccpetRequest(int Id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var friendRequest = repo.GetFriendRequestById(Id);
            if (friendRequest == null)
                return NotFound();

            if (friendRequest.UserSenderId == user.Id)
            {
                return BadRequest("You cannot accept your own friend request.");
            }
            friendRequest.Status = "accepted";
            var request = repo.UpdateFriendRequest(friendRequest);
            return Ok(request);
        }


        [HttpPost("RejectRequest")]
        [Authorize]
        public async Task<ActionResult<Frinds>> RejectRequest(int Id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var data = repo.GetFriendRequestById(Id);
            if (data == null)
                return NotFound();

            data.Status = "rejected";
            var request = repo.UpdateFriendRequest(data);
            return Ok(request);
        }


        [HttpGet("GetFrindsUser")]
        [Authorize]
        public async Task<ActionResult> GetFrindsUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Unauthorized();

            var data = await repo.GetUserFrinds(user.Id);
            if (data == null) return NotFound();

            return Ok(data);

        }

    }
}
