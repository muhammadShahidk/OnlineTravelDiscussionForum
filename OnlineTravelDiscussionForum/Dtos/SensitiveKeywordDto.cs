using OnlineTravelDiscussionForum.Modals;
using System.ComponentModel.DataAnnotations;

namespace OnlineTravelDiscussionForum.Dtos
{
    public class SensitiveKeywordRequestDto
    {
        public string Keyword { get; set; }

    }

    public class SensitiveKeywordResponseDto: SensitiveKeywordRequestDto
    {
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; } = null;
        public int Id { get; set; }
    }
}
