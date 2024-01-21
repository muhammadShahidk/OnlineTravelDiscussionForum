using OnlineTravelDiscussionForum.Dtos;
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
        public Task<List<bandUserResponceDto>> GetAllBannedUsers();
        Task<bandUserResponceDto> BanUser(bandUserRequestDto banUser);
        Task<bandUserResponceDto> ChangeBandStatus(ChangeBandStatusDto banUser);
        Task<bool> isUserBand(string userId);
        Task<List<bandUserResponceDto>> GetBanndUserHistery(string userId);
        Task<List<bandUserResponceDto>> GetAllBannedUniqueHistery();
        Task<List<BandUsersStatusResponceDto>> GetAllUsersStatus();
    }
}
