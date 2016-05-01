namespace M101DotNet.Services.Models
{
    using System;

    public class CommentServiceModel
    {
        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public int Likes { get; set; }
    }
}