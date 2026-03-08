using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Post
{
    /// <summary>
    /// Post-ийн үндсэн класс
    /// </summary>
    public abstract class PostBase
    {
        /// <summary>ID дугаар</summary>
        public required PostId Id { get; init; }
        /// <summary>Бичсэн хэрэглэгчийн ID дугаар</summary>
        public required UserId AuthorId { get; init; }
        /// <summary>Харагдацын төрөл</summary>
        public required VisibilityType Visibility { get; set; }
        /// <summary>Post үүсгэсэн огноо</summary>
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}