using AutoMapper;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostResponseDto>().ReverseMap();
            CreateMap<Post, PostRequestDto>().ReverseMap();

            CreateMap<Comment, CommentResposnceDto>().ReverseMap();
            CreateMap<Comment, CommentRequestDto>().ReverseMap();


            CreateMap<ApprovalRequest, ApprovalResponseDto>().ReverseMap();
            CreateMap<ApprovalRequest, ApprovalRequestDto>().ReverseMap();

            CreateMap<SensitiveKeyword, SensitiveKeywordResponseDto>().ReverseMap();
            CreateMap<SensitiveKeyword, SensitiveKeywordRequestDto>().ReverseMap();

        }
    }
}
