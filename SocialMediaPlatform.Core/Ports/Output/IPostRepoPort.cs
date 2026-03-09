using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Domain.Post;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Post-ийн Repository-н Output Port интерфейс
    /// </summary>
    public interface IPostRepoPort
    {
        /// <summary>Post хадгалах</summary>
        /// <param name="post">Хадгалах Post-ийн объект</param>
        public void Save(PostBase post);

        /// <summary>ID-аар Post хайх</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Олдсон Post-ийн объект</returns>
        public PostBase FindById(PostId postId);

        /// <summary>
        /// Платформ дээрх бүх Post-уудыг авах
        /// </summary>
        /// <returns>Post-ийн объектын жагсаалт</returns>
        public List<PostBase> GetAll();

        /// <summary>Хэрэглэгчийн Post-уудыг хайх</summary>
        /// <param name="userId">Хэрэглэгчийн ID дугаар</param>
        /// <returns>Post-ийн объектын жагсаалт</returns>
        public List<PostBase> FindByAuthor(UserId userId);

        /// <summary>Post шинэчлэх</summary>
        /// <param name="post">Шинэчлэх Post-ийн объект</param>
        public void Update(PostBase post);

        /// <summary>Post устгах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        public void Delete(PostId postId);
    }
}