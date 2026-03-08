using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Post
{
    /// <summary>
    /// Post-ийн үндсэн абстракт класс
    /// </summary>
    public abstract class Post<TPostType>
    {
        /// <summary>ID дугаар</summary>
        public required PostId Id { get; init; }

        /// <summary>Post үүсгэсэн огноо</summary>
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        /// <summary>Бичсэн хэрэглэгчийн ID дугаар</summary>
        public required UserId AuthorId { get; init; }

        /// <summary>Харагдацын төрөл</summary>
        public required VisibilityType Visibility { get; set; }

        /// <summary>Post-ийн төрөл</summary>
        public required TPostType Type { get; init; }
    }
}