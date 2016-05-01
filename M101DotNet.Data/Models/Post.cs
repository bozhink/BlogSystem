﻿namespace M101DotNet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Post
    {
        public Post()
        {
            this.Comments = new List<Comment>();
            this.CreatedAtUtc = DateTime.UtcNow;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string[] Tags { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}