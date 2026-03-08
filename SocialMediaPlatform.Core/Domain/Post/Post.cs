using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Post
{
    /// <summary>
    /// Post-ийн үндсэн абстракт ерөнхий төрлийн класс
    /// </summary>
    public abstract class Post<TPostType> : PostBase
    {
        public required TPostType Type { get; init; }
    }
}