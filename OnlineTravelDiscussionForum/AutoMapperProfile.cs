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
                     .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                      .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.FirstName));



            CreateMap<Comment, CommentRequestDto>().ReverseMap();


            CreateMap<ApprovalResponseDto, ApprovalRequest>().ReverseMap();
            CreateMap<ApprovalRequest, ApprovalResponseDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.User.FirstName));

            CreateMap<ApprovalRequest, ApprovalRequestDto>().ReverseMap();

            CreateMap<SensitiveKeyword, SensitiveKeywordResponseDto>().ReverseMap();
            CreateMap<SensitiveKeyword, SensitiveKeywordRequestDto>().ReverseMap();

            CreateMap<ApplicationUser, UserResponseDto>().ReverseMap();
            CreateMap<ApplicationUser, UserRequestDto>().ReverseMap();

            CreateMap<BandUser, bandUserResponceDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.user.UserName))
                      .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.user.FirstName))
                      .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.user.LastName))
                      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.user.Email))
                      .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.user.Id));

            CreateMap<bandUserResponceDto, BandUser>();

            CreateMap<bandUserRequestDto, BandUser>().ReverseMap();

            //CreateMap<ApplicationUser, BandUsersStatusResponceDto>()
            //    .ForMember(dest =>dest.Status , opt=>opt.MapFrom(src =>{
            //                var isband = src.BandUsers
            //                                .FirstOrDefault(x => x.Status == BandStatus.Active);
            //                var status = isband != null ? isband.Status : BandStatus.Inactive;
            //                //return status;
            //            }));
            CreateMap<ChangeBandStatusDto, BandUser>();


            CreateMap<Reply, ReplyResponseDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<ReplyResponseDto, Reply>();


            CreateMap<Reply, ReplyRequestDto>().ReverseMap();


        }
    }
}
