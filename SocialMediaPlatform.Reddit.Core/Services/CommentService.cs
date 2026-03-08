using SocialMediaPlatform.Core.Domain.Comment;
using SocialMediaPlatform.Core.Domain.DTO;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Core.Ports.Input;
using SocialMediaPlatform.Core.Ports.Output;
using SocialMediaPlatform.Reddit.Core.Domain.Comment;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Services
{
    /// <summary>
    /// Comment-ийн үйлдлүүдийг гүйцэтгэх Service класс
    /// </summary>
    public class CommentService : ICommentServicePort
    {
        private readonly ICommentRepoPort _repo;
        private readonly IIdGeneratorPort _idGenerator;

        /// <summary>
        /// CommentService үүсгэх
        /// </summary>
        /// <param name="repo">Comment-ийн Repository адаптер</param>
        /// <param name="idGenerator">ID үүсгэгч</param>
        public CommentService(ICommentRepoPort repo, IIdGeneratorPort idGenerator)
        {
            _repo = repo;
            _idGenerator = idGenerator;
        }

        /// <summary>
        /// Post-д Comment нэмэх
        /// </summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн Comment-ийн DTO</returns>
        public CommentDTO AddComment(PostId postId, UserId authorId, string content)
        {
            var id = _idGenerator.NextCommentId();
            var comment = new MainComment
            {
                Id = id,
                AuthorId = authorId,
                Content = content,
                Type = CommentType.Main,
                PostId = postId
            };
            _repo.Save(comment);
            return ToDTO(comment);
        }

        /// <summary>
        /// Comment-д хариу бичих
        /// </summary>
        /// <param name="commentId">Эх Comment-ийн ID дугаар</param>
        /// <param name="authorId">Бичсэн хэрэглэгчийн ID дугаар</param>
        /// <param name="content">Агуулга</param>
        /// <returns>Үүсгэгдсэн Reply Comment-ийн DTO</returns>
        public CommentDTO ReplyToComment(CommentId commentId, UserId authorId, string content)
        {
            var id = _idGenerator.NextCommentId();
            var comment = new ReplyComment
            {
                Id = id,
                AuthorId = authorId,
                Content = content,
                Type = CommentType.Reply,
                ParentCommentId = commentId
            };
            _repo.Save(comment);
            return ToDTO(comment);
        }

        /// <summary>
        /// Comment устгах
        /// </summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        public void DeleteComment(CommentId commentId)
        {
            _repo.Delete(commentId);
        }

        /// <summary>
        /// Post-ийн Comment-уудыг авах
        /// </summary>
        /// <param name="postId">Post-ийн ID дугаар</param>
        /// <returns>Comment-ийн DTO жагсаалт</returns>
        public List<CommentDTO> GetComments(PostId postId)
        {
            return _repo.FindByPost(postId)
                .Select(ToDTO)
                .ToList();
        }

        /// <summary>
        /// Comment-ийн хариунуудыг авах
        /// </summary>
        /// <param name="commentId">Comment-ийн ID дугаар</param>
        /// <returns>Reply Comment-ийн DTO жагсаалт</returns>
        public List<CommentDTO> GetReplies(CommentId commentId)
        {
            return _repo.FindByParent(commentId)
                .Select(ToDTO)
                .ToList();
        }

        /// <summary>
        /// Comment объектыг DTO болгох
        /// </summary>
        /// <param name="comment">Comment-ийн объект</param>
        /// <returns>Comment-ийн DTO</returns>
        private static CommentDTO ToDTO(CommentBase comment)
        {
            PostId? postId = null;
            CommentId? parentCommentId = null;

            if (comment is MainComment mc) postId = mc.PostId;
            if (comment is ReplyComment rc) parentCommentId = rc.ParentCommentId;

            return new CommentDTO(
                comment.Id,
                comment.AuthorId,
                comment.Content,
                comment.CreatedAt,
                postId,
                parentCommentId
            );
        }
    }
}