namespace M101DotNet.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Models;

    using Data.Common.Repositories.Contracts;
    using Data.Models;

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

        public Task<object> AddNewPost(PostServiceModel post)
        {
            throw new NotImplementedException();
        }

        public Task<PostServiceModel> GetPostById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<PostServiceModel>> GetPosts()
        {
            return (await this.repository.All())
                .OrderByDescending(p => p.CreatedAtUtc)
                .Take(10)
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

        public Task<IQueryable<TagServiceModel>> GetTags()
        {
            throw new NotImplementedException();
        }

        public Task<object> LikeComment(string postId, int index)
        {
            throw new NotImplementedException();
        }
    }
}