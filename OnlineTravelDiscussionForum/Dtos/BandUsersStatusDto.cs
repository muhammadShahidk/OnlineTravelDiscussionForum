using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Dtos
{
    public class BandUsersStatusResponceDto
    {
        public string userId { get; set; } = default!;
        public string? userName { get; set; } = default!;
        public BandStatus Status { get; set; } = default!;
    }
}
