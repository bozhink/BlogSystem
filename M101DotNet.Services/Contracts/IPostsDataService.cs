namespace M101DotNet.Services.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public interface IPostsDataService
    {
        Task<object> AddNewComment(string postId, CommentServiceModel comment);

        Task<object> AddNewPost(PostServiceModel post);

        Task<PostServiceModel> GetPostById(string id);

        Task<IQueryable<PostServiceModel>> GetPosts(int skip, int take);

        Task<IQueryable<PostServiceModel>> GetPostsByTag(string tag);

        Task<IQueryable<TagServiceModel>> GetTags();

        Task<object> LikeComment(string postId, int index);
    }
}