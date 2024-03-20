using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IReplyService
    {
        Task<ReplyResponseDto> CreateReplyAsync(int commentId, ReplyRequestDto reply);
        Task<ReplyResponseDto> GetReplyByIdAsync(int replyId);
        Task<List<ReplyResponseDto>> GetRepliesByCommentIdAsync(int commentId);
        Task<ReplyResponseDto> UpdateReplyAsync(int id, ReplyRequestDto reply);
        Task<bool> DeleteReplyAsync(int replyId);
    }
}
