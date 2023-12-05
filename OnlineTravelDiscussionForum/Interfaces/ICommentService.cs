using OnlineTravelDiscussionForum.Dtos;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface ICommentService
    {
        Task<string> DeleteComment(int commentId);
        Task<string> UpdateComment(int commentId, CommentRequestDto comment);
        Task<IEnumerable<CommentResposnceDto>> GetComments();


    }
}
