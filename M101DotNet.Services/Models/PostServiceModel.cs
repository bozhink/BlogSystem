namespace M101DotNet.Services.Models
{
    using System;
    using System.Collections.Generic;

    public class PostServiceModel
    {
        public PostServiceModel()
        {
            this.Comments = new List<CommentServiceModel>();
            this.CreatedAtUtc = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string[] Tags { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public ICollection<CommentServiceModel> Comments { get; set; }
    }
}