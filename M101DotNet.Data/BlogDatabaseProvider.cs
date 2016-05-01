namespace M101DotNet.Data
{
    using System;
    using System.Configuration;
    using Contracts;
    using MongoDB.Driver;

    public class BlogDatabaseProvider : IBlogDatabaseProvider
    {
        public const string ConnectionStringName = "Blog";
        public const string DatabaseName = "blog";
        public const string PostsCollectionName = "posts";
        public const string UsersCollectionName = "users";

        public IMongoDatabase Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(DatabaseName);
            return database;
        }
    }
}