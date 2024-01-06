using AutoMapper;
using SocialMedia.Api.DTO;
using SocialMedia.Core.Entity.Post;
using SocialMedia.Core.Identity;

namespace SocialMedia.Api.Hellper
{
    public class MapeProfile : Profile
    {
        public MapeProfile() 
        {
            CreateMap<CommentDto, Comment>()
                .ForMember(dest => dest.PhotoComment, opt => opt.MapFrom(src => src.PhotoComment));


            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<Post, PostDtoreturn>()
                .ForMember(dest => dest.likes, opt => opt.MapFrom(src => src.likes!.Count()));

            CreateMap<Comment, CommentDto>()
                 .ForMember(dest => dest.PhotoComment, opt => opt.MapFrom(src => src.PhotoComment));


            CreateMap<AppUser , UserGetDto>().ReverseMap();
           
        }

    }


}
