using SocialMediaPlatform.Core.Domain.Comment;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Ports.Output
{
    /// <summary>
    /// Comment-ийн Repository-н Output Port интерфейс
    /// </summary>
    public interface ICommentRepoPort
    {
        /// <summary>Comment хадгалах</summary>
        /// <param name="comment">Хадгалах comment-ийн объект</param>
        public void Save(Comment<object> comment);

        /// <summary>ID-аар comment хайх</summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        /// <returns>Олдсон comment-ийн объект</returns>
        public Comment<object> FindById(CommentId commentId);

        /// <summary>Post-ийн comment-уудыг авах</summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Comment-ийн объектын жагсаалт</returns>
        public List<Comment<object>> FindByPost(PostId postId);

        /// <summary>Comment-ийн хариунуудыг авах</summary>
        /// <param name="commentId">Эх comment-ийн ID дугаар</param>
        /// <returns>Reply comment-ийн объектын жагсаалт</returns>
        public List<Comment<object>> FindByParent(CommentId commentId);

        /// <summary>Comment устгах</summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        public void Delete(CommentId commentId);
    }
}