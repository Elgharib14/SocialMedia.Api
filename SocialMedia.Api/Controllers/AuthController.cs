using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Api.DTO;
using SocialMedia.Api.Hellper;
using SocialMedia.Core.Entity;
using SocialMedia.Core.Entity.Post;
using SocialMedia.Core.Identity;
using SocialMedia.Core.Reposatory;
using SocialMedia.Core.Services;
using SocialMedia.Reposatory.AppContext;
using SocialMedia.Reposatory.Identitycontext;
using System.Security.Claims;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbcontext appDbcontext;
        private readonly IMapper mapper;
        private readonly AppIdentityDbContext dbContext;
        private readonly IGenericRepo<Post> genericRepo;
        private readonly IGenericRepo<Image> imageRepo;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenServices tokenServices;
        


        public AuthController(AppDbcontext appDbcontext,IMapper mapper ,AppIdentityDbContext dbContext,IGenericRepo<Post> genericRepo , IGenericRepo<Image> ImageRepo, UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ITokenServices tokenServices)
        {
            this.appDbcontext = appDbcontext;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.genericRepo = genericRepo;
            imageRepo = ImageRepo;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
        }

        
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RigsterDto rigsterDto)
        {
            if (await userManager.FindByEmailAsync(rigsterDto.Email!) is not null)
                return  BadRequest("Email is already Registered" );
            var user = new AppUser()
            {
                FirstName = rigsterDto.FirstName,
                LastName = rigsterDto.LastName,
                City = rigsterDto.City,
                Country = rigsterDto.Country,
                PhoneNumber = rigsterDto.Phone,
                Email = rigsterDto.Email,
                UserName = rigsterDto.Email.Split('@')[0]
            };

            var result = await userManager.CreateAsync(user, rigsterDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                return BadRequest(errors);
            }

           
            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Bio = user.Bio,
                UserImage = user.UserImage,
                Token = await tokenServices.CreatUserToken(user, userManager)
            });

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized();
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized();


            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Bio = user.Bio,
                UserImage = user.UserImage,
                Token = await tokenServices.CreatUserToken(user, userManager), 
            });
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromForm]UserUpdateDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) return Unauthorized();

            user.Bio = dto.Bio;
            user.UserName = dto.UserName;
            user.UserImage = AddFile.UploadFile(dto.UserImage, "UserImage");
            user.City = dto.City;
            user.Country = dto.Country;

            var newImage = new Image
            {
                UserId = user.Id,
                ImageUrl = user.UserImage
            };
            var image = imageRepo.Creat(newImage);

            var newpost = new Post
            {
                UserId = user.Id,
                Photo = user.UserImage,
            };
            var post = genericRepo.Creat(newpost);


            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                return BadRequest(errors);
            }
            return Ok(new UserDto()
            {
                Bio = user.Bio,
                UserImage = user.UserImage,
                UserName = user.UserName,
                Email = user.Email,
            });
        }

        [HttpGet] 
        public async Task<ActionResult<IReadOnlyList<UserGetDto>>> GetAllUser()
        {
            var data = await dbContext.Users.ToListAsync();
            var map = mapper.Map<IReadOnlyList<UserGetDto>>(data);
            return Ok(map);
        }



    }
}
