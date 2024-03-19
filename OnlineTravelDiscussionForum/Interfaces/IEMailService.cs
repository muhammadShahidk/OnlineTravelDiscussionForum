using OnlineTravelDiscussionForum.Dtos;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IEMailService
    {
        bool SendEmail(EmailDto request);
    }
}
