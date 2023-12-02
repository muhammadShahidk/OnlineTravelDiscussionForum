namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IUserService
    {
        public Task  GetCurrentUserId();
        public Task UpdatePassword();
    }
}
