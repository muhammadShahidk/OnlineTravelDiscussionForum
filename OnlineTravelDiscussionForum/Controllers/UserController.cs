using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.OtherObjects;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;



        public UserController(UserManager<ApplicationUser> userManager, ForumDbContext context, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/users/approval-request
        [HttpPost("approval-request")]
        [Authorize(Roles = StaticRoles.USER)]

        public async Task<ActionResult<ApprovalResponseDto>> ApprovalRequest()
        {
            try
            {


                var LogedinUserID = await _userService.GetCurrentUserId();
               
                if (LogedinUserID == null)
                {
                    return BadRequest("first login to see posts");
                }
                
                var user =  await _userManager.FindByIdAsync(LogedinUserID);
                if (user == null)
                {
                    return BadRequest("user not found");
                }
                //check if your aleady have a pending request
                var pendingRequest = _context.ApprovalRequests.FirstOrDefault(request => request.UserID == LogedinUserID);
                if (pendingRequest != null)
                {
                    return BadRequest("you already have a pending request");
                }
                var approvalRequestObj = new ApprovalRequest
        {
                    UserID = LogedinUserID,
                    Status = ApprovalStatus.Pending,
                    DateCreated = DateTime.Now
                };
                await _context.ApprovalRequests.AddAsync(approvalRequestObj);
                await _context.SaveChangesAsync();

                return Ok(new ApprovalResponseDto
                {
                    UserID = approvalRequestObj.UserID,
                    Status = approvalRequestObj.Status,
                    DateCreated = approvalRequestObj.DateCreated,
                    RequestId = approvalRequestObj.RequestId
                });

        }
            catch (Exception)
            {

                throw;
            }


        }


        [HttpPut("approval-request")]
        [Authorize(Roles = StaticRoles.USER)]

        public async Task<ActionResult<ApprovalResponseDto>> ApprovalRequest(ApprovalRequestDto approvalRequest)
        {
            var LogedinUserID = CurrentUserID();
            if (LogedinUserID == null)
            {
                return BadRequest("first login to see posts");
            }
            var user = await _userManager.FindByIdAsync(LogedinUserID);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            //aprove request
            var approvalRequestObj = _context.ApprovalRequests.FirstOrDefault(request => request.RequestId == approvalRequest.RequestId);
            if (approvalRequestObj == null)
            {
                return BadRequest("request not found");
            }
            approvalRequestObj.Status = approvalRequest.Status;
            approvalRequestObj.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new ApprovalResponseDto
            {
                UserID = approvalRequestObj.UserID,
                Status = approvalRequestObj.Status,
                DateCreated = approvalRequestObj.DateCreated,
                DateUpdated = approvalRequestObj.DateUpdated,
                RequestId = approvalRequestObj.RequestId
            });





        }

        [HttpGet("approval-request")]
        [Authorize(Roles = StaticRoles.USER)]
        public async Task<ActionResult<ApprovalResponseDto>> GetAllAprovalRequests()
        {
            var AllRequests = await _context.ApprovalRequests.ToListAsync();
            var approvalStatus = ApprovalStatus.Pending.ToString();
            if (AllRequests == null)
            {
                return BadRequest("no requests found");
        }
            var AllRequestsDto = _mapper.Map<List<ApprovalResponseDto>>(AllRequests);

            return Ok(AllRequestsDto);


        }
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
