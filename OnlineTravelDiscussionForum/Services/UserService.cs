using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Migrations;
using OnlineTravelDiscussionForum.Modals;
using System.Diagnostics;
using System.Security.Claims;

namespace OnlineTravelDiscussionForum.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ForumDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;



        public UserService(IHttpContextAccessor httpContextAccessor, ForumDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bandUserResponceDto> BanUser(bandUserRequestDto bandUser)
        {
            var user = await _userManager.FindByIdAsync(bandUser.userId);
            if (user == null)
            {
                throw new Exception("user not found");
            }
            var newBanduser = new BandUser()
            {
                startDate = bandUser.startDate,
                endDate = bandUser.endDate,
                Status = BandStatus.Active,
                UserID = user.Id,

            };

            //find if user is already banned and have active status
            var bandUserExist = await _context.BandUsers.FirstOrDefaultAsync(x => x.UserID == newBanduser.UserID && x.Status == BandStatus.Active);
            if (bandUserExist != null)
            {
                throw new Exception("user already banned");
            }

            try
            {
                var result = await _context.BandUsers.AddAsync(newBanduser);
                await _context.SaveChangesAsync();

                //var buser = await _context.BandUsers.FirstAsync(x => x.UserID == newBanduser.UserID);
                return _mapper.Map<bandUserResponceDto>(result.Entity);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bandUserResponceDto> ChangeBandStatus(ChangeBandStatusDto banUser)
        {
            var user = await _context.BandUsers.Include(x => x.user).Where(x => x.UserID == banUser.userId && x.Status == BandStatus.Active).FirstOrDefaultAsync();
            
            
            if (user == null)
            {
                throw new Exception($"user is already {BandStatus.Inactive}");
            }

            user.Status = BandStatus.Inactive;
            await _context.SaveChangesAsync();
            return _mapper.Map<bandUserResponceDto>(user);

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

        public async Task<List<bandUserResponceDto>> GetAllBannedUsers()
        {
            var banndUsers = await _context.BandUsers.Include(x => x.user).Where(x => x.Status == BandStatus.Active).ToListAsync();
            if (banndUsers == null)
            {
                return null;
            }

            var banndUsersDto = _mapper.Map<List<bandUserResponceDto>>(banndUsers);
            return banndUsersDto;

        }
        public async Task<List<bandUserResponceDto>> GetBanndUserHistery(string userId)
        {
            var banndUsers = await _context.BandUsers.Include(x => x.user)
                   .Where(x =>
                          x.UserID == userId).ToListAsync();

            if (banndUsers == null)
            {
                return null;
            }

            var banndUsersDto = _mapper.Map<List<bandUserResponceDto>>(banndUsers);
            return banndUsersDto;
        }
        public async Task<List<bandUserResponceDto>> GetAllBannedUniqueHistery()
        {
            //all unique users who are banned
            var bannedUsers = await _context.BandUsers.Include(x => x.user)
                .GroupBy(x => x.UserID)
                .Select(group => group.First())
                .ToListAsync();


            if (bannedUsers == null)
            {
                return null;
            }

            var banndUsersDto = _mapper.Map<List<bandUserResponceDto>>(bannedUsers);
            return banndUsersDto;
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

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;

        }

        public List<string> GetUserRools()
        {
            var user = _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var roles = _userManager.GetRolesAsync(user.Result);
            return roles.Result.ToList();
        }

        public async Task<bool> isUserBand(string userId)
        {
            var user = await _context.BandUsers.FirstOrDefaultAsync(x => x.UserID == userId && x.Status == BandStatus.Active);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public Task UpdatePassword()
        {
            throw new NotImplementedException();
        }

        public async Task<List<BandUsersStatusResponceDto>> GetAllUsersStatus()
        {
            var AllUsersWithStatus2 = await _context.Users
                       .Select(user => new BandUsersStatusResponceDto
                       {
                           userId = user.Id,
                           userName = user.UserName,
                           Status = user.BandUsers.Any(status => status.Status == BandStatus.Active)
                                       ? BandStatus.Active
                                       : BandStatus.Inactive
                       })
                       .ToListAsync();


            return AllUsersWithStatus2;
        }
    }
}
