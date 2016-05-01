namespace M101DotNet.Services
{
    using System;
    using System.Linq;
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

            var entity = new Post
            {
                Author = post.Author,
                Content = post.Content,
                CreatedAtUtc = post.CreatedAtUtc,
                Tags = post.Tags,
                Title = post.Title
            };

            var result = await this.repository.Add(entity);

            post.Id = entity.Id;

            return result;
        }

        public Task<PostServiceModel> GetPostById(string id)
        {
            throw new NotImplementedException();
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
                .Select(p => new PostServiceModel
                {
                    Author = p.Author,
                    Comments = p.Comments.Select(c => new CommentServiceModel
                    {
                        Author = c.Author,
                        Content = c.Content,
                        CreatedAtUtc = c.CreatedAtUtc,
                        Likes = c.Likes
                    }).ToList(),
                    Content = p.Content,
                    CreatedAtUtc = p.CreatedAtUtc,
                    Id = p.Id,
                    Tags = p.Tags,
                    Title = p.Title
                })
                .AsQueryable();
        }

        public Task<IQueryable<PostServiceModel>> GetPostsByTag(string tag)
        {
            throw new NotImplementedException();
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