using AspNetCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;
using System.ComponentModel.Design;

namespace OnlineTravelDiscussionForum.Services
{
    public class ReplyService : IReplyService
    {
        private readonly ForumDbContext _forumDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ReplyService> _logger;
        private readonly IUserService _userService;

        public ReplyService(ForumDbContext forumDbContext, IMapper mapper, ILogger<ReplyService> logger, IUserService userService)
        {
            _forumDbContext = forumDbContext;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }



        public async Task<ReplyResponseDto> CreateReplyAsync(int commentId, ReplyRequestDto reply)
        {

            var userId = await _userService.GetCurrentUserId();
            // Check if the comment exists
            var comment = await _forumDbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);


            if (comment == null)
            {
                _logger.LogError($"Comment with ID {commentId} does not exist.");
                throw new ArgumentException($"Comment with ID {commentId} does not exist.");
            }

            var newReply = _mapper.Map<Reply>(reply);
            newReply.CommentId = commentId;
            newReply.UserID = userId;

            _forumDbContext.Replies.Add(newReply);
            await _forumDbContext.SaveChangesAsync();

            return _mapper.Map<ReplyResponseDto>(newReply);


        }

        public async Task<bool> DeleteReplyAsync(int replyId)
        {
            var reply = await _forumDbContext.Replies.FindAsync(replyId);
            if (reply == null)
            {
                _logger.LogError($"_reply with ID {replyId} does not exist.");
                throw new ArgumentException($"_reply with ID {replyId} does not exist.");
                
            }
            _forumDbContext.Replies.Remove(reply);
            await _forumDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReplyResponseDto>> GetRepliesByCommentIdAsync(int commentId)
        {
            // Check if the comment exists
            var commentExists = await _forumDbContext.Comments.AnyAsync(c => c.CommentId == commentId);
            if (!commentExists)
            {
                _logger.LogError($"Comment with ID {commentId} does not exist.");
                throw new ArgumentException($"Comment with ID {commentId} does not exist.");
            }

            var replies = await _forumDbContext.Replies.Include(x => x.User)
                .Where(r => r.CommentId == commentId)
                .ToListAsync();

            return _mapper.Map<List<ReplyResponseDto>>(replies);
        }

        public async Task<ReplyResponseDto> GetReplyByIdAsync(int replyId)
        {
            var reply = await _forumDbContext.Replies.FindAsync(replyId);
            return _mapper.Map<ReplyResponseDto>(reply);
        }

        public async Task<ReplyResponseDto> UpdateReplyAsync(int id, ReplyRequestDto content)
        {
            var _reply = await _forumDbContext.Replies.FindAsync(id);
            if (_reply == null)
            {
                _logger.LogError($"_reply with ID {id} does not exist.");
                throw new ArgumentException($"_reply with ID {id} does not exist.");
            }
            _reply.Content = content.Content;
            _forumDbContext.Entry(_reply).State = EntityState.Modified;

            await _forumDbContext.SaveChangesAsync();

            return _mapper.Map<ReplyResponseDto>(_reply);

        }
    }
}
