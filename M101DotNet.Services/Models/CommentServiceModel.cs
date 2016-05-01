namespace M101DotNet.Services.Models
{
    using System;

    public class CommentServiceModel
    {
        public CommentServiceModel()
        {
            this.CreatedAtUtc = DateTime.UtcNow;
            this.Likes = 0;
        }

        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public int Likes { get; set; }
    }
}