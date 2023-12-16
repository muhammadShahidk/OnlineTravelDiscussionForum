using OnlineTravelDiscussionForum.Dtos;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IEMailService
    {
        void SendEmail(EmailDto request);
    }
}
