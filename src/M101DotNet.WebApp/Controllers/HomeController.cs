namespace M101DotNet.WebApp.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Services.Contracts;
    using Services.Models;

    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IPostsDataService service;

        public HomeController(IPostsDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<ActionResult> Index()
        {
            var recentPosts = (await this.service.GetPosts(0, 10)).ToList();

            var tags = (await this.service.GetTags()).ToList();

            var model = new IndexModel
            {
                RecentPosts = recentPosts,
                Tags = tags
            };

            return this.View(model);
        }

        [HttpGet]
        public ActionResult NewPost()
        {
            return this.View(new NewPostModel());
        }

        [HttpPost]
        public async Task<ActionResult> NewPost(NewPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var post = new PostServiceModel
            {
                Author = User.Identity.Name,
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags.Split(' ', ',', ';').Where(t => !string.IsNullOrWhiteSpace(t)).ToArray()
            };

            await this.service.AddNewPost(post);

            return this.RedirectToAction(nameof(this.Post), new { id = post.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var post = await this.service.GetPostById(id);

            if (post == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var model = new PostModel
            {
                Post = post,
                NewComment = new NewCommentModel
                {
                    PostId = id
                }
            };

            return this.View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var posts = (await this.service.GetPostsByTag(tag)).ToList();
            return this.View(posts);
        }

        [HttpPost]
        public async Task<ActionResult> NewComment(NewCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Post), new { id = model.PostId });
            }

            var comment = new CommentServiceModel
            {
                Author = User.Identity.Name,
                Content = model.Content
            };

            await this.service.AddNewComment(model.PostId, comment);

            return this.RedirectToAction(nameof(this.Post), new { id = model.PostId });
        }

        [HttpPost]
        public async Task<ActionResult> CommentLike(CommentLikeModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Post), new { id = model.PostId });
            }

            await this.service.LikeComment(model.PostId, model.CommentId);

            return this.RedirectToAction(nameof(this.Post), new { id = model.PostId });
        }
    }
}