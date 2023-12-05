using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineTravelDiscussionForum.Data;
using OnlineTravelDiscussionForum.Dtos;
using OnlineTravelDiscussionForum.Interfaces;
using OnlineTravelDiscussionForum.Modals;

namespace OnlineTravelDiscussionForum.Services
{
    public class PostsService : IPostsService
    {
        private readonly ForumDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;


        public PostsService(ForumDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<List<PostResponseDto>> GetPosts(string? userId)
        {
            if (userId == null)
            {
                return _mapper.Map<List<PostResponseDto>>(await _context.Posts.ToListAsync());

            }
            else
            {
                return _mapper.Map<List<PostResponseDto>>(await _context.Posts.Where(x => x.UserID == userId).ToListAsync());

            }
        }

        public async Task<PostResponseDto> GetPost(int id, string? userId)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                throw new InvalidOperationException("Post not found");
            }
            else if (userId != null && post.UserID != userId)
            {
                throw new InvalidOperationException("You are not allowed to see this post");
            }


            return _mapper.Map<PostResponseDto>(post);

        }

        public async Task<string> UpdatePost(int id, PostRequestDto post)
        {
            var currentPost = await _context.Posts.FindAsync(id);

            if (currentPost == null)
            {
                throw new InvalidOperationException("Post not found");
            }

            // Your logic for updating the post

            return "Post updated";
        }

        public async Task<string> AddPost(PostRequestDto post, string? userId)
        {
            // Your logic for adding a new post

            return "Post added: -> " + post.Title;
        }

        public async Task<string> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                throw new InvalidOperationException("Post not found");
            }

            // Your logic for deleting the post

            return $"Post {id} deleted";
        }

        // Methods for comments
        public async Task<string> AddComment(CommentRequestDto comment, int postId, string userId)
        {
            // Your logic for adding a comment to a post

            return "Comment added";
        }

        public async Task<string> DeleteComment(int commentId, string userId)
        {
            // Your logic for deleting a comment

            return $"Comment {commentId} deleted";
        }

        public async Task<string> UpdateComment(int commentId, CommentRequestDto comment, string userId)
        {
            // Your logic for updating a comment

            return $"Comment {commentId} updated";
        }

        public Task<List<CommentResposnceDto>> GetComments(int postId, string userID)
        {
            throw new NotImplementedException();
        }

        public Task<CommentResposnceDto> GetComment(int commentId, string userID)
        {
            throw new NotImplementedException();
        }





    }


}
