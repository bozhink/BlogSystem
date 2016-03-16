namespace Blog.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public virtual int PostId { get; set; }

        public virtual Post Post { get; set; }

        public virtual string UserId { get; set; }

        public virtual User User { get; set; }
    }
}