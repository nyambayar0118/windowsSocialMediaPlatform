using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Reaction
{
    /// <summary>
    /// Reaction-ий үндсэн абстракт класс
    /// </summary>
    public abstract class Reaction<TReactionType>
    {
        /// <summary>Reaction-ийг дарсан зүйлийн ID дугаар</summary>
        public required uint TargetId { get; init; }

        /// <summary>Reaction-ийг дарсан зүйлийн төрөл</summary>
        public required ReactionTargetType TargetType { get; init; }

        /// <summary>Бичсэн хэрэглэгчийн ID дугаар</summary>
        public required UserId AuthorId { get; init; }

        /// <summary>Үүсгэсэн огноо</summary>
        public DateTime CreatedAt { get; init; } = DateTime.Now;

        /// <summary>Reaction-ий төрөл</summary>
        public required TReactionType Type { get; init; }
    }
}