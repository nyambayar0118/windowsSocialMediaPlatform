using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Input
{
    /// <summary>
    /// Comment-ийн үйлдлүүдийн Input Port интерфейс
    /// </summary>
    public interface ICommentServicePort
    {
        /// <summary>Comment нэмэх</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн comment-ийн DTO</returns>
        public CommentDTO AddComment(PostId postId, UserId authorId, string content);

        /// <summary>Comment-д хариу бичих</summary>
        /// <param name="commentId">Эх comment-ийн ID дугаар</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн reply comment-ийн DTO</returns>
        public CommentDTO ReplyToComment(CommentId commentId, UserId authorId, string content);

        /// <summary>Comment устгах</summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        public void DeleteComment(CommentId commentId);

        /// <summary>Post-ийн comment-уудыг авах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Comment-ийн DTO жагсаалт</returns>
        public List<CommentDTO> GetComments(PostId postId);
        /// <summary>
        /// ID дугаараар Comment-ийг авах
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns>Comment DTO</returns>
        public CommentDTO GetComment(CommentId commentId);

        /// <summary>Comment-ийн хариунуудыг авах</summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        /// <returns>Reply comment-ийн DTO жагсаалт</returns>
        public List<CommentDTO> GetReplies(CommentId commentId);
    }
}