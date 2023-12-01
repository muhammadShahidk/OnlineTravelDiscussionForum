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
          
        }
    }
}
