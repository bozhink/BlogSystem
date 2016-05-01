namespace M101DotNet.Data
{
    using System.Configuration;
    using Models;
    using MongoDB.Driver;

    public class BlogContext
    {
        public const string ConnectionStringName = "Blog";
        public const string DatabaseName = "blog";
        public const string PostsCollectionName = "posts";
        public const string UsersCollectionName = "users";

        // This is ok... Normally, they would be put into
        // an IoC container.
        private readonly IMongoClient client;

        private readonly IMongoDatabase database;

        public BlogContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            this.client = new MongoClient(connectionString);
            this.database = this.client.GetDatabase(DatabaseName);
        }

        public IMongoClient Client => this.client;

        public IMongoCollection<Post> Posts => this.database.GetCollection<Post>(PostsCollectionName);

        public IMongoCollection<User> Users => this.database.GetCollection<User>(UsersCollectionName);
    }
}