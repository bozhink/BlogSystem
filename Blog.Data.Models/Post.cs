namespace Blog.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        private ICollection<Comment> comments;
        private ICollection<Tag> tags;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public virtual int AuthorId { get; set; }

        public virtual Author Author { get; set; }

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

        public virtual ICollection<Tag> Tags
        {
            get
            {
                return this.tags;
            }

            set
            {
                this.tags = value;
            }
        }
    }
}