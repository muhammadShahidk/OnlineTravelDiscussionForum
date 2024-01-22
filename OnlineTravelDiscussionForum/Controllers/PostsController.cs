using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.OtherObjects;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ForumDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(ForumDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        //all posts 
        [HttpGet]
        [Authorize(Roles = $"{StaticRoles.USER}")]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts()
        {

           
            var posts = await _context.Posts.Include(c=>c.user).ToListAsync();
               
            if(posts == null)
            {
                return NotFound("no posts to show");
            }   
            return _mapper.Map<List<PostResponseDto>>(posts); ;

        }



        //get post by id
        [HttpGet("{id}"), Authorize(Roles = $"{StaticRoles.USER}")]
        public async Task<ActionResult<PostResponseDto>> GetPost(int id)
        {
            var post = await _context.Posts.Include(c=>c.user).FirstAsync(x=>x.PostID == id);

            if (post == null)
            {
                return NotFound("no post exist");
            }

            return _mapper.Map<PostResponseDto>(post);
        }

      
        //Delete post
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("no post exist");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

           return Ok("post deleted");
           
        }
       
        

        //get post comments
        [Authorize(Roles = StaticRoles.USER)]
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments(int id)
        {
                var userId = await _userService.GetCurrentUserId();
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid post ID");
                }

                if (userId == null)
                {
                    return BadRequest("Please log in to view comments");
                }

                var postWithComments = await _context.Posts
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                    .FirstOrDefaultAsync(p => p.PostID == id);

                if (postWithComments == null)
                {
                    return NotFound($"Post {id} was not found");
                }


                var commentsForPost = postWithComments.Comments.ToList();
                var commentResponseDtos = _mapper.Map<List<CommentResposnceDto>>(commentsForPost);
                //foreach (var item in commentResponseDtos)
                //{
                //    if (commentsForPost[0].User == null)
                //    {
                //        break;
                //    }
                //    item.Username = commentsForPost.Find(x=>x.UserID == item.UserID).User.UserName;  
                //    item.name = commentsForPost.Find(x => x.UserID == item.UserID).User.FirstName;
                //}

                return Ok(commentResponseDtos);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // logger.LogError(ex, "An error occurred while getting comments.");

                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
