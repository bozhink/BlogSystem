﻿namespace M101DotNet.WebApp.Models
{
    using MongoDB.Bson.Serialization.Attributes;

    public class TagProjection
    {
        [BsonElement("_id")]
        public string Name { get; set; }

        public int Count { get; set; }
    }
}