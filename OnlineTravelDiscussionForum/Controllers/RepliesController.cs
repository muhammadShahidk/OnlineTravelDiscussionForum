using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineTravelDiscussionForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RepliesController : ControllerBase
    {

        private readonly IReplyService _replyService; // Inject your reply service here
        private readonly ForumDbContext _forumDbContext;
        private readonly IMapper _mapper;

        public RepliesController(IReplyService replyService)
        {
            _replyService = replyService;
        }


        // GET: api/<RepliesController>
        [HttpGet("comment/{commentId}")]
        public async Task<ActionResult<List<ReplyResponseDto>>> GetReplyesByComment(int commentId)
        {
            try
            {
                var replies = await _replyService.GetRepliesByCommentIdAsync(commentId);
                return Ok(replies);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //// GET api/<RepliesController>/5
        //[HttpGet("")]
        //public string GetUserReplies(int id)
        //{
        //    return "value";
        //}



        // POST api/<RepliesController>

        [HttpPost("comment/{commentId}")]
        public async Task<ActionResult> CreateReply(int commentId, ReplyRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            // Call service method to create reply
            try
            {
                var createdReply = await _replyService.CreateReplyAsync(commentId, requestDto);
                //return CreatedAtAction(nameof(GetReply), new { id = responseDto.ReplyId }, responseDto);
                return Ok(createdReply);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }

        // PUT api/<RepliesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ReplyRequestDto value)
        {
            try
            {

                var updatedReply = await _replyService.UpdateReplyAsync(id, value);
                return Ok(updatedReply);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<RepliesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                var isDeleted = await _replyService.DeleteReplyAsync(id);
                return Ok("Replied Deleted");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
