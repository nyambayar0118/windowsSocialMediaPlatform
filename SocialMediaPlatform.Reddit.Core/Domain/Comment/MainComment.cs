using SocialMediaPlatform.Core.Domain.Comment;
using SocialMediaPlatform.Core.Domain.ID;
using SocialMediaPlatform.Reddit.Core.Enum;

namespace SocialMediaPlatform.Reddit.Core.Domain.Comment
{
    /// <summary>
    /// Post-д шууд бичигдэх үндсэн Comment класс
    /// </summary>
    public class MainComment : Comment<CommentType>
    {
        /// <summary>Харьяалагдах Post-ийн ID дугаар</summary>
        public required PostId PostId { get; init; }
    }
}