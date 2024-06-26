﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace OnlineTravelDiscussionForum.Modals
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DateCreateAt { get; set; } = DateTime.Now;
        public DateTime? DateUpdateAt { get; set; }

        public string UserID { get; set; }

        public ApplicationUser User { get; set; }

        public int PostID { get; set; }

        public Post Post { get; set; }

        // Navigation property for replies
        public ICollection<Reply> Replies { get; set; } = new List<Reply>();

    }


}
