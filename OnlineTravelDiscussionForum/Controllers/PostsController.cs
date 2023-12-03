﻿using System;
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

        // GET: api/Posts  get posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts()
        {

            var userid = await _userService.GetCurrentUserId();
            if (userid == null)
            {
                return BadRequest("first login to see posts");
            }
            var posts = await _context.Posts.Where(x => x.UserID == userid).ToListAsync();
            //PostResponseDto[] postsResponse = posts.Select(x=>new PostResponseDto {Id = x.PostID, Title = x.Title , Content = x.Content }).ToArray();

            return _mapper.Map<List<PostResponseDto>>(posts); ;

        }

        // GET: api/Posts/5 get post by id
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseDto>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return _mapper.Map<PostResponseDto>(post); ;
        }

        // PUT: api/Posts/5 update by id 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PostRequestDto post)
        {
            if (post == null)
            {
                return BadRequest("add new content ");
            }

            var CurrentPost = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id);

            if (CurrentPost == null)
            {
                return NotFound("no post exist");
            }

            var sensitiveKeywords = await _context.SensitiveKeywords.ToListAsync();

            CurrentPost.Title = _userService.FilterSensitiveWords(post.Title, sensitiveKeywords) ?? CurrentPost.Title;
            CurrentPost.Content = _userService.FilterSensitiveWords(post.Content, sensitiveKeywords) ?? CurrentPost.Content;

            _context.Entry(CurrentPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("post updated");
        }

        // POST: api/Posts new post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = StaticRoles.USER)]
        public async Task<ActionResult<Post>> PostPost(PostRequestDto post)
        {

            if (post == null)
            {
                return BadRequest("no data to add");
            }

            var userId = await _userService.GetCurrentUserId();
            if (userId != null)
            {
                //var currentUser = 
                //check if user is using sensitive words in the post then first remove the sensitive words and then add the post
                var sensitiveWords = await _context.SensitiveKeywords.ToListAsync();
                post.Content = _userService.FilterSensitiveWords(post.Content, sensitiveWords);
                post.Title = _userService.FilterSensitiveWords(post.Title, sensitiveWords);


                var newPost = _context.Posts.Add(new Post { Title = post.Title, Content = post.Content, UserID = userId });

            }

            await _context.SaveChangesAsync();

            return Ok("post added: -> " + post.Title);
        }



        private string? CurrentUserID()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        // DELETE: api/Posts/5 delete by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok($"post {post.Title} :-> Deleted");
        }
        [HttpPost("test{id}")]
        public async Task<IActionResult> test(int id)
        {
            return (Ok("id:" + id));
        }

        [HttpPut("multiple")]
        public IActionResult GetMultipleData([FromHeader] string header1, [FromBody] dynamic bodyData, [FromQuery] string? param1 = null)
        {
            var result = new
            {
                Param1 = param1,
                Header1 = header1,
                BodyData = bodyData
            };

            return Ok(result);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }

        [Authorize(Roles = StaticRoles.USER)]
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> addComments(int id, [FromBody] CommentRequestDto comment)
        {
            if (id == null)
            {
                return BadRequest("please provide post id");
            }
            var userid = await _userService.GetCurrentUserId();
            if (userid == null)
            {
                return BadRequest("please login to create comments");
            }

            var post = _context.Posts.Single(e => e.PostID == id);
            if (post == null)
            {
                return BadRequest($"Post {id} was not found");
            }

            //filtering sensitive words from the comment
            var sensitiveWords = await _context.SensitiveKeywords.ToListAsync();
            comment.Content = _userService.FilterSensitiveWords(comment.Content, sensitiveWords);

            var NewComment = new Comment { UserID = userid, Content =  comment.Content, PostID = post.PostID };



            post.Comments.Add(NewComment);
            _context.SaveChanges();

            return (Ok("comments are addedz"));
        }



        [Authorize(Roles = StaticRoles.USER)]
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid post ID");
                }

                var userId = await _userService.GetCurrentUserId();
                if (userId == null)
                {
                    return BadRequest("Please log in to view comments");
                }

                var postWithComments = await _context.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.PostID == id);

                if (postWithComments == null)
                {
                    return NotFound($"Post {id} was not found");
                }

                var commentsForPost = postWithComments.Comments.ToList();
                var commentResponseDtos = _mapper.Map<List<CommentResposnceDto>>(commentsForPost);

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
