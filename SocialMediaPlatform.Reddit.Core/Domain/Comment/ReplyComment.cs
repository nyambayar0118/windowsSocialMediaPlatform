using SocialMediaPlatform.Core.Domain.Comment;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Domain.Comment
{
    /// <summary>
    /// Comment-д хариу болгон бичигдэх Reply Comment класс
    /// </summary>
    public class ReplyComment : Comment<CommentType>
    {
        /// <summary>Харьяалагдах эх Comment-ийн ID дугаар</summary>
        public required CommentId ParentCommentId { get; init; }
    }
}