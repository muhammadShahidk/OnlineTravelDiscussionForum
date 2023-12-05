using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IUserService
    {
        public Task<string>  GetCurrentUserId();
        public Task<ApplicationUser> GetCurrentUser();
        public Task UpdatePassword();
        public string FilterSensitiveWords(string Content, List<SensitiveKeyword> sensitiveWords);
        public List<string> GetUserRools();
    }
}
