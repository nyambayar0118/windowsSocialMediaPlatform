using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Post;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Post;
using SocialMediaPlatform.Reddit.Core.Enum;
using SocialMediaPlatform.Reddit.Core.Factories;

namespace SocialMediaPlatform.Reddit.Core.Services
{
    /// <summary>
    /// Post-ийн үйлдлүүдийг гүйцэтгэх Service класс
    /// </summary>
    public class PostService : IPostServicePort
    {
        private readonly IPostRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;
        private readonly PostFactory _factory;

        /// <summary>
        /// PostService үүсгэх
        /// </summary>
        /// <param name="repo">Post-ийн Repository адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        /// <param name="factory">Post үүсгэх Factory</param>
        public PostService(IPostRepoPort repo, IIdGeneratorPort idGenerator, PostFactory factory)
        {
            _repo = repo;
            _idGenerator = idGenerator;
            _factory = factory;
        }

        /// <summary>
        /// Post үүсгэх
        /// </summary>
        /// <param name="type">Post-ийн төрөл</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн Post-ийн DTO</returns>
        public PostDTO CreatePost(string type, UserId authorId, string content)
        {
            var id = _idGenerator.NextPostId();
            var postType = System.Enum.Parse<PostType>(type);
            var post = _factory.Create(postType, authorId, id, content, content);
            _repo.Save(post);
            return ToDTO(post);
        }

        /// <summary>
        /// Post устгах
        /// </summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        public void DeletePost(PostId postId)
        {
            _repo.Delete(postId);
        }

        /// <summary>
        /// Post засах
        /// </summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <param name="content">Шинэ агуулга</param>
        /// <returns>Засагдсан Post-ийн DTO</returns>
        public PostDTO EditPost(PostId postId, string content)
        {
            var post = _repo.FindById(postId);
            if (post is TimelinePost tp) tp.Content = content;
            else if (post is SubredditPost sp) sp.Content = content;
            _repo.Update(post);
            return ToDTO(post);
        }

        /// <summary>
        /// Post авах
        /// </summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Post-ийн DTO</returns>
        public PostDTO GetPost(PostId postId)
        {
            var post = _repo.FindById(postId);
            return ToDTO(post);
        }

        /// <summary>
        /// Хэрэглэгчийн timeline авах
        /// </summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Post-ийн DTO жагсаалт</returns>
        public List<PostDTO> GetTimeline(UserId userId)
        {
            return _repo.FindByAuthor(userId)
                .Select(ToDTO)
                .ToList();
        }

        /// <summary>
        /// Post объектыг DTO болгох
        /// </summary>
        /// <param name="post">Post-ийн объект</param>
        /// <returns>Post-ийн DTO</returns>
        private static PostDTO ToDTO(PostBase post)
        {
            GroupId? subredditId = null;
            string? title = null;

            if (post is SubredditPost sp)
            {
                subredditId = sp.SubredditId;
                title = sp.Title;
            }
            else if (post is TimelinePost tp)
            {
                title = tp.Title;
            }

            return new PostDTO(post.Id, post.AuthorId, post.Visibility, post.CreatedAt, title, subredditId);
        }
    }
}