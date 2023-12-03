using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using OnlineTravelDiscussionForum.OtherObjects;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ForumDbContext _context;
        private readonly IUserService _userService;

        public CommentsController(ForumDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, CommentRequestDto comment)
        {
           //update existing comment with id 
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
                    .FirstOrDefaultAsync(c => c.CommentId == id);

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


        // DELETE: api/Comments/5
        [Authorize(Roles = StaticRoles.USER)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid comment ID");
                }

                var userId = await _userService.GetCurrentUserId();

                var commentToDelete = await _context.Comments
                    .FirstOrDefaultAsync(c => c.CommentId == id );


                if (commentToDelete == null)
                {
                    return NotFound($"Comment {id} was not found");
                }
               
                //admin can delete any comment
                var userRools = _userService.GetUserRools();
                if(userRools.Contains(StaticRoles.ADMIN) || userRools.Contains(StaticRoles.MODERATOR))
                {
                    _context.Comments.Remove(commentToDelete);
                    await _context.SaveChangesAsync();
                    return Ok("Comment deleted successfully");
                }

                // Check if the user is the owner of the comment
                if (commentToDelete.UserID != userId) {
                
                    return BadRequest("you are not allowed to delete this comment");
                }
                // Remove the comment from the database
                _context.Comments.Remove(commentToDelete);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok("Comment deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // logger.LogError(ex, "An error occurred while deleting the comment.");

                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
