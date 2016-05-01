namespace M101DotNet.WebApp.Models
{
    using System.Configuration;
    using MongoDB.Driver;

    public class BlogContext
    {
        public const string CONNECTION_STRING_NAME = "Blog";
        public const string DATABASE_NAME = "blog";
        public const string POSTS_COLLECTION_NAME = "posts";
        public const string USERS_COLLECTION_NAME = "users";

        // This is ok... Normally, they would be put into
        // an IoC container.
        private static readonly IMongoClient client;

        private static readonly IMongoDatabase database;

        static BlogContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            client = new MongoClient(connectionString);
            database = client.GetDatabase(DATABASE_NAME);
        }

        public IMongoClient Client
        {
            get { return client; }
        }

        public IMongoCollection<Post> Posts
        {
            get { return database.GetCollection<Post>(POSTS_COLLECTION_NAME); }
        }

        public IMongoCollection<User> Users
        {
            get { return database.GetCollection<User>(USERS_COLLECTION_NAME); }
        }
    }
}