namespace Blog.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class BlogDbContext : IdentityDbContext<User>
    {
        public BlogDbContext()
            : base("BlogDbContext", throwIfV1Schema: false)
        {
        }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public static BlogDbContext Create()
        {
            return new BlogDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}