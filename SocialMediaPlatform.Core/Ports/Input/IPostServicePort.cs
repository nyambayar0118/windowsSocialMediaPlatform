using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Post-ийн үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface IPostServicePort
    {
        /// <summary>Post үүсгэх</summary>
        /// <param name="type">Post-ийн төрөл</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн Post-ийн DTO</returns>
        public PostDTO CreatePost(string type, UserId authorId, string content);

        /// <summary>Post устгах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        public void DeletePost(PostId postId);

        /// <summary>Post засах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <param name="content">Шинэ агуулга</param>
        /// <returns>Засагдсан Post-ийн DTO</returns>
        public PostDTO EditPost(PostId postId, string content);

        /// <summary>Post авах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Post-ийн DTO</returns>
        public PostDTO GetPost(PostId postId);

        /// <summary>Хэрэглэгчийн timeline авах</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Post-ийн DTO жагсаалт</returns>
        public List<PostDTO> GetTimeline();
    }
}