using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Dtos
{
    public class bandUserResponceDto

    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public BandStatus Status { get; set; }
    }
    
    public class bandUserRequestDto
    {
        public string userId { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        //public BandStatus Status { get; set; }
    }

    public class ChangeBandStatusDto
    {
        public string userId { get; set; } 
        //public BandStatus Status { get; set; }
    }


}
