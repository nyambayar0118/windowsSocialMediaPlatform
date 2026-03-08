using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Reaction
{
    /// <summary>
    /// Reaction-ий үндсэн абстракт ерөнхий төрлийн класс
    /// </summary>
    public abstract class Reaction<TReactionType> : ReactionBase
    {
        /// <summary>Reaction-ий төрөл</summary>
        public required TReactionType Type { get; init; }
    }
}