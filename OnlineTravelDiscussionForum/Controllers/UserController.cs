using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.OtherObjects;
using System.CodeDom;
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

        //user informations 
        [HttpGet]
        [Authorize(Roles = $"{StaticRoles.USER},{StaticRoles.ADMIN}")]

        public async Task<ActionResult<UserResponseDto>> GetUserDetails()
        {
            var LogedinUserID = await _userService.GetCurrentUserId();
            if (LogedinUserID == null)
            {
                return BadRequest("first login to see posts");
            }
            var user = await _userManager.FindByIdAsync(LogedinUserID);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            var userResponse = _mapper.Map<UserResponseDto>(user);
            var rolls = await _userManager.GetRolesAsync(user);
            if (rolls == null)
            {
                return BadRequest("no rools");

            }

            userResponse.Rools.AddRange(rolls);

            return Ok(userResponse);

        }


        //get all users 
        [HttpGet("all")]
        [Authorize(Roles = $"{StaticRoles.MODERATOR}")]
        public async Task<ActionResult<UserResponseDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                return BadRequest("no users found");
            }
            var usersResponse = _mapper.Map<List<UserResponseDto>>(users);
            foreach (var user in users)
            {
                var rolls = await _userManager.GetRolesAsync(user);
                if (rolls == null)
                {
                    return BadRequest("no rools");

                }

                var currentuser = usersResponse.Find(x => x.Username == user.UserName);
                currentuser?.Rools.AddRange(rolls);
            }
            return Ok(usersResponse);

        }


        //approval requests handling

        [HttpPost("approval-request")]

        public async Task<ActionResult<ApprovalResponseDto>> ApprovalRequest([FromBody] ARequestDto username)
        {
            try
            {


                //var LogedinUserID = await _userService.GetCurrentUserId();
                var user = await _userManager.FindByNameAsync(username.username);

                if (user == null)
                {
                    return BadRequest("please provide Correct username");
                }

                //var user =  await _userManager.FindByIdAsync(LogedinUserID);
                //if (user == null)
                //{
                //    return BadRequest("user not found");
                //}
                //check if your aleady have a pending request
                var pendingRequest = _context.ApprovalRequests.Include(x => x.User).FirstOrDefault(request => request.UserID == user.Id);
                if (pendingRequest != null)
                {
                    return BadRequest("you already have a pending request");
                }
                var approvalRequestObj = new ApprovalRequest
                {
                    UserID = user.Id,
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
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]

        public async Task<ActionResult<ApprovalResponseDto>> ApprovalRequest(ApprovalRequestDto approvalRequest)
        {
            var LogedinUserID = await _userService.GetCurrentUserId();
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
            var approvalRequestObj = _context.ApprovalRequests.Include(x => x.User).FirstOrDefault(request => request.RequestId == approvalRequest.RequestId);
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
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]

        public async Task<ActionResult<ApprovalResponseDto>> GetAllAprovalRequests()
        {
            var AllRequests = await _context.ApprovalRequests.Include(x => x.User).ToListAsync();
            var approvalStatus = ApprovalStatus.Pending.ToString();
            if (AllRequests == null)
            {
                return BadRequest("no requests found");
            }
            var AllRequestsDto = _mapper.Map<List<ApprovalResponseDto>>(AllRequests);

            return Ok(AllRequestsDto);


        }


        //sensitive keywords handling

        [HttpGet("sensitivekeyword")]
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]

        public async Task<ActionResult<List<SensitiveKeywordResponseDto>>> GetAllSensitiveKeywords()
        {
            List<SensitiveKeyword> AllKeywords = await _context.SensitiveKeywords.ToListAsync();
            if (AllKeywords == null || AllKeywords.Count <= 0)
            {
                return BadRequest("no keywords found");
            }
            return Ok(_mapper.Map<List<SensitiveKeywordResponseDto>>(AllKeywords));
        }

        [HttpPost("sensitivekeyword")]
        //[Authorize(Roles = StaticRoles.USER)]
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]

        public async Task<ActionResult<SensitiveKeywordResponseDto>> AddSensitiveKeyword(SensitiveKeywordRequestDto sensitiveKeywordDto)
        {
            var LogedinUserID = await _userService.GetCurrentUserId();
            if (LogedinUserID == null)
            {
                return BadRequest("first login to see posts");
            }
            var user = await _userManager.FindByIdAsync(LogedinUserID);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            var keyword = await _context.SensitiveKeywords.FirstOrDefaultAsync(keyword => keyword.Keyword == sensitiveKeywordDto.Keyword);
            if (keyword != null)
            {
                return BadRequest("keyword already exists");
            }
            var newKeyword = new SensitiveKeyword
            {
                Keyword = sensitiveKeywordDto.Keyword,
                DateCreated = DateTime.Now,
                UserId = LogedinUserID,
            };
            await _context.SensitiveKeywords.AddAsync(newKeyword);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<SensitiveKeywordResponseDto>(newKeyword));
        }


        [HttpDelete("sensitivekeyword/{id}")]
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]
        public async Task<ActionResult> DeleteSensitiveKeyword(int id)
        {
            var keyword = await _context.SensitiveKeywords.FirstOrDefaultAsync(keyword => keyword.Id == id);
            if (keyword == null)
            {
                return BadRequest("keyword not found");
            }
            _context.SensitiveKeywords.Remove(keyword);
            await _context.SaveChangesAsync();
            return Ok("keyword deleted");
        }

        [HttpPut("sensitivekeyword/{id}")]
        [Authorize(Roles = $"{StaticRoles.ADMIN}")]
        public async Task<ActionResult> UpdateSensitiveKeyword(int id, SensitiveKeywordRequestDto sensitiveKeywordDto)
        {
            var keyword = await _context.SensitiveKeywords.FirstOrDefaultAsync(keyword => keyword.Id == id);
            if (keyword == null)
            {
                return BadRequest("keyword not found");
            }
            keyword.Keyword = sensitiveKeywordDto.Keyword;
            keyword.DateUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok("keyword updated");
        }



        //user posts handling 

        [HttpGet("posts")]
        [Authorize(Roles = $"{StaticRoles.USER}")]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts()
        {

            var userid = await _userService.GetCurrentUserId();
            if (userid == null)
            {
                return BadRequest("first login to see posts");
            }
            var posts = await _context.Posts.Where(x => x.UserID == userid).Include(c => c.user).ToListAsync();
            //PostResponseDto[] postsResponse = posts.Select(x=>new PostResponseDto {Id = x.PostID, Title = x.Title , Content = x.Content }).ToArray();

            return _mapper.Map<List<PostResponseDto>>(posts); ;

        }



        [HttpPut("posts/{id}")]
        [Authorize(Roles = $"{StaticRoles.USER}")]
        public async Task<IActionResult> PutPost(int id, PostRequestDto post)
        {
            var userid = await _userService.GetCurrentUserId();
            if (post == null)
            {
                return BadRequest("add new content ");
            }

            //getting current  post of current user  from the database
            var CurrentPost = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id && x.UserID == userid);

            if (CurrentPost == null)
            {
                return NotFound("no post exist");
            }

            //getting the sensitive words from the database
            var sensitiveKeywords = await _context.SensitiveKeywords.ToListAsync();

            //filtering the sensitive words from the post
            if (post.Title != null)
            {
                post.Title = _userService.FilterSensitiveWords(post.Title, sensitiveKeywords);
            }
            if (post.Content != null)
            {
                post.Content = _userService.FilterSensitiveWords(post.Content, sensitiveKeywords);
            }

            CurrentPost.Title = post.Title ?? CurrentPost.Title;
            CurrentPost.Content = post.Content ?? CurrentPost.Content;


            //save the changes
            _context.Entry(CurrentPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //send server internal error
                return StatusCode(500);

            }

            return Ok("post updated");
        }



        [HttpPost("posts")]
        [Authorize(Roles = StaticRoles.USER)]
        public async Task<ActionResult<Post>> PostPost(PostRequestDto post)
        {
            var userId = await _userService.GetCurrentUserId();

            if (post == null)
            {
                return BadRequest("no data to add");
            }

            if (userId != null)
            {
                //check if the user is banned
                bool status = await _userService.isUserBand(userId);

                if (status)
                {
                    return BadRequest("you are banned");
                }

                //filtering sensitive words from the post
                var sensitiveWords = await _context.SensitiveKeywords.ToListAsync();
                post.Content = _userService.FilterSensitiveWords(post.Content, sensitiveWords);
                post.Title = _userService.FilterSensitiveWords(post.Title, sensitiveWords);


                _context.Posts.Add(new Post { Title = post.Title, Content = post.Content, UserID = userId });
                await _context.SaveChangesAsync();

            }


            return Ok("post added: -> " + post.Title);
        }




        [HttpDelete("posts/{id}")]
        [Authorize(Roles = $"{StaticRoles.USER}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userid = await _userService.GetCurrentUserId();
            if (userid == null)
            {
                return BadRequest("first login to see posts");
            }
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id && x.UserID == userid);
            if (post == null)
            {
                return NotFound("no post exist");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok("post deleted");
        }



        //user comments handling

        [Authorize(Roles = StaticRoles.USER)]
        [HttpPost("posts/{id}/comments")]
        public async Task<IActionResult> addComments(int id, [FromBody] CommentRequestDto comment)
        {
            var userid = await _userService.GetCurrentUserId();

            //exception handling
            if (userid == null)
            {
                return BadRequest("first login to see posts");
            }

            //check if the user is banned
            bool status = await _userService.isUserBand(userid);
            if (status)
            {
                return BadRequest("you are banned");
            }

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostID == id);
            if (post == null)
            {
                return NotFound("no post exist");
            }
            if (comment == null)
            {
                return BadRequest("no data to add");
            }

            //filtering sensitive words from the comment
            var sensitiveWords = await _context.SensitiveKeywords.ToListAsync();
            comment.Content = _userService.FilterSensitiveWords(comment.Content, sensitiveWords);

            //adding comment to post
            _context.Comments.Add(new Comment { Content = comment.Content, UserID = userid, PostID = id });

            await _context.SaveChangesAsync();

            return Ok(new { message = "comment added", comment = comment.Content });

        }



        [Authorize(Roles = StaticRoles.USER)]
        [HttpGet("posts/{id}/comments")]
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

                //get current user posts with comments
                var postWithComments = await _context.Posts
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                    .FirstOrDefaultAsync(p => p.PostID == id && p.UserID == userId);

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




        [Authorize(Roles = StaticRoles.USER)]
        [HttpPut("comments/{id}")]
        public async Task<IActionResult> PutComment(int id, CommentRequestDto comment)
        {
            var userId = await _userService.GetCurrentUserId();
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid comment ID");
                }

                if (comment == null)
                {
                    return BadRequest("Please provide a valid comment object");
                }

                var commentToUpdate = await _context.Comments
                    .FirstOrDefaultAsync(c => c.CommentId == id && c.UserID == userId);

                if (commentToUpdate == null)
                {
                    return NotFound($"Comment {id} was not found");
                }

                // Update the comment object with the values from the request
                commentToUpdate.Content = comment.Content;
                commentToUpdate.DateUpdateAt = DateTime.Now;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("Comment updated successfully");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // logger.LogError(ex, "An error occurred while updating the comment.");

                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }




        [Authorize(Roles = StaticRoles.USER)]
        [HttpGet("comments")]
        public async Task<IActionResult> GetAllComments()
        {
            var userId = await _userService.GetCurrentUserId();
            try
            {
                if (userId == null)
                {
                    return BadRequest("Please log in to view comments");
                }

                //get current user all comments
                var comments = await _context.Comments.Where(x => x.UserID == userId).ToListAsync();


                if (comments == null)
                {
                    return NotFound($"comments was not found");
                }

                var commentResponseDtos = _mapper.Map<List<CommentResposnceDto>>(comments);

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
