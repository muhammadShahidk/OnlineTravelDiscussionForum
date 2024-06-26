﻿using OnlineTravelDiscussionForum.Dtos;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsync();
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto);
        Task<List<UserRoleDto>> GetUserRolsAsync();
        Task<bool> CheckIfUserIsApproved(LoginDto loginDto);
        Task<AuthServiceResponseDto> MakeUserAsync(UpdatePermissionDto updatePermissionDto);
    }
}
