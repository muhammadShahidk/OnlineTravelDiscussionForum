using OnlineTravelDiscussionForum.Dtos;

namespace OnlineTravelDiscussionForum.Interfaces
{
    public interface IPostsService
    {
        Task<List<PostResponseDto>> GetPosts(string? userId);
        Task<PostResponseDto> GetPost(int id,string? userID);
        Task<string> UpdatePost(int id, PostRequestDto post);
        Task<string> AddPost(PostRequestDto post,string? userID);
        Task<string> DeletePost(int id);

        // methods for comments
        Task<string> AddComment(CommentRequestDto comment, int postId,string userID);
        Task<string> DeleteComment(int commentId , string userID);
        Task<string> UpdateComment(int commentId, CommentRequestDto comment , string UserID);
        Task<List<CommentResposnceDto>> GetComments(int postId,string userID);
        Task<CommentResposnceDto> GetComment(int commentId,string userID);

    }
}
