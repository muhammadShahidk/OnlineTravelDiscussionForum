using AutoMapper;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostResponseDto, Post>();
            CreateMap<Post, PostResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.user.UserName))
             .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.user.FirstName));


            CreateMap<Post, PostRequestDto>().ReverseMap();

            CreateMap<CommentResposnceDto, Comment>();
            CreateMap<Comment, CommentResposnceDto>()
                     .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.FirstName))
             .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.FirstName));



            CreateMap<Comment, CommentRequestDto>().ReverseMap();


            CreateMap<ApprovalRequest, ApprovalResponseDto>().ReverseMap();
            CreateMap<ApprovalRequest, ApprovalRequestDto>().ReverseMap();

            CreateMap<SensitiveKeyword, SensitiveKeywordResponseDto>().ReverseMap();
            CreateMap<SensitiveKeyword, SensitiveKeywordRequestDto>().ReverseMap();

            CreateMap<ApplicationUser, UserResponseDto>().ReverseMap();
            CreateMap<ApplicationUser, UserRequestDto>().ReverseMap();

        }
    }
}
