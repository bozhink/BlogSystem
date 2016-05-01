namespace M101DotNet.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using Data.Common.Repositories.Contracts;
    using Data.Models;

    using Models;

    public class PostsDataService : IPostsDataService
    {
        private readonly IGenericRepository<Post> repository;

        public PostsDataService(IGenericRepository<Post> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        private Func<Comment, CommentServiceModel> CommentToServiceModel => c => new CommentServiceModel
        {
            Author = c.Author,
            Content = c.Content,
            CreatedAtUtc = c.CreatedAtUtc,
            Likes = c.Likes
        };

        private Func<CommentServiceModel, Comment> CommentToDataModel => c => new Comment
        {
            Author = c.Author,
            Content = c.Content,
            CreatedAtUtc = c.CreatedAtUtc,
            Likes = c.Likes
        };

        private Func<Post, PostServiceModel> PostToServiceModel => p => new PostServiceModel
        {
            Id = p.Id,
            Author = p.Author,
            Content = p.Content,
            CreatedAtUtc = p.CreatedAtUtc,
            Title = p.Title,
            Tags = p.Tags,
            Comments = p.Comments.Select(this.CommentToServiceModel).ToList()
        };

        private Func<PostServiceModel, Post> PostToDataModel => p => new Post
        {
            Id = p.Id,
            Author = p.Author,
            Content = p.Content,
            CreatedAtUtc = p.CreatedAtUtc,
            Title = p.Title,
            Tags = p.Tags,
            Comments = p.Comments.Select(this.CommentToDataModel).ToList()
        };

        public Task<object> AddNewComment(string postId, CommentServiceModel comment)
        {
            throw new NotImplementedException();
        }

        public async Task<object> AddNewPost(PostServiceModel post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            var entity = this.PostToDataModel.Invoke(post);

            var result = await this.repository.Add(entity);

            post.Id = entity.Id;

            return result;
        }

        public async Task<PostServiceModel> GetPostById(string id)
        {
            var entity = (await this.repository.Get(id));
            return this.PostToServiceModel.Invoke(entity);
        }

        public async Task<IQueryable<PostServiceModel>> GetPosts(int skip, int take)
        {
            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip));
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            return (await this.repository.All())
                .OrderByDescending(p => p.CreatedAtUtc)
                .Skip(skip)
                .Take(take)
                .ToList()
                .Select(this.PostToServiceModel)
                .AsQueryable();
        }

        public async Task<IQueryable<PostServiceModel>> GetPostsByTag(string tag)
        {
            Expression<Func<Post, bool>> filter = x => true;

            if (!string.IsNullOrWhiteSpace(tag))
            {
                filter = x => x.Tags.Contains(tag);
            }

            var posts = (await this.repository.All())
                .Where(filter)
                .OrderByDescending(p => p.CreatedAtUtc)
                .ToList()
                .Select(this.PostToServiceModel);

            return posts.AsQueryable();
        }

        // TODO: needs revision
        public async Task<IQueryable<TagServiceModel>> GetTags()
        {
            var tags = (await this.repository.All())
                .Select(p => new
                {
                    Id = p.Id,
                    Tags = p.Tags
                })
                .ToList()
                .SelectMany(p => p.Tags.Select(t => new
                {
                    Id = p.Id,
                    Tag = t
                }))
                .GroupBy(x => x.Tag)
                .Select(g => new TagServiceModel
                {
                    Name = g.Key,
                    Count = g.Select(x => x.Id).Distinct().Count()
                });

            return tags.AsQueryable();
        }

        public Task<object> LikeComment(string postId, int index)
        {
            throw new NotImplementedException();
        }
    }
}