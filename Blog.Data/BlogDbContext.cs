namespace Blog.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Models;

    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
            : base("BlogDbContext")
        {
        }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}