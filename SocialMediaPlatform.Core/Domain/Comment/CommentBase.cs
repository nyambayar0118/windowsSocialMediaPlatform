using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.Comment
{
    /// <summary>
    /// Comment-ийн цөм абстракт класс
    /// </summary>
    public abstract class CommentBase
    {
        public required CommentId Id { get; init; }
        public required UserId AuthorId { get; init; }
        public required string Content { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.Now;
    }
}