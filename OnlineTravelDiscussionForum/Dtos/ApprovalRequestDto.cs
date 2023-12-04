using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Dtos
{
    public class ARequestDto
    {
        public string  username { get; set; }
    }
    public class ApprovalRequestDto
    {

        public ApprovalStatus Status { get; set; }
        public int RequestId { get; set; }

    }

    public class ApprovalResponseDto : ApprovalRequestDto
    {
        public DateTime DateCreated { get; set; } 
        public DateTime? DateUpdated { get; set; }
        public string UserID { get; set; }



    }
}
