using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ForumDbContext _context;

        public CommentsController(ForumDbContext context)
        {
            _context = context;
        }

       
        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

                var commentToUpdate = await _context.Comments
                    .FirstOrDefaultAsync(c => c.CommentId == id);

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid comment ID");
                }

               

                var commentToDelete = await _context.Comments
                    .FirstOrDefaultAsync(c => c.CommentId == id);

                if (commentToDelete == null)
                {
                    return NotFound($"Comment {id} was not found");
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
