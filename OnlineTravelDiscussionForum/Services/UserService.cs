using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using System.Security.Claims;

namespace OnlineTravelDiscussionForum.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ForumDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public UserService(IHttpContextAccessor httpContextAccessor, ForumDbContext context, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
        }

        public string FilterSensitiveWords(string Content, List<SensitiveKeyword> sensitiveWords)
        {
                foreach (var word in sensitiveWords)
                {
                    var keyword = word.Keyword.ToLower();

                    if (Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        //Content.Replace(word.Keyword, "***");
                        Content = Content.Replace(keyword, "***", StringComparison.OrdinalIgnoreCase);

                    }
                }
                return Content;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var CurrentUser = await _userManager.FindByIdAsync(await GetCurrentUserId());
            if (CurrentUser == null)
            {
            
            }   

            return CurrentUser;

        }

        public async Task<string> GetCurrentUserId()
        {
            
            var userId =  _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           return userId;

        }

        public List<string> GetUserRools()
        {
            var user = _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var roles = _userManager.GetRolesAsync(user.Result);
            return roles.Result.ToList();
        }

        public Task UpdatePassword()
        {
            throw new NotImplementedException();
        }

      
    }
}
