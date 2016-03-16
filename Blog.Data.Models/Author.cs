namespace Blog.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        private ICollection<Post> posts;
        private ICollection<Comment> comments;

        public Author()
        {
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(60)]
        public string Email { get; set; }

        [MaxLength(60)]
        public string Password { get; set; }

        public virtual ICollection<Post> Posts
        {
            get
            {
                return this.posts;
            }

            set
            {
                this.posts = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }
    }
}