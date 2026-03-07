using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Comment классын объектын Data Transfer Object
    /// </summary>
    /// <param name="Id">ID дугаар</param>
    /// <param name="AuthorId">Бичсэн хэрэглэгчийн ID дугаар</param>
    /// <param name="Content">Агуулга</param>
    /// <param name="CreatedAt">Үүсгэсэн огноо</param>
    /// <param name="PostId">Харьяалагдах Post-ийн ID дугаар</param>
    /// <param name="ParentCommentId">Харьяалагдах Comment-ийн ID дугаар</param>
    public record CommentDTO(
        CommentId Id,
        UserId AuthorId,
        string Content,
        DateTime CreatedAt,
        PostId? PostId = null,
        CommentId? ParentCommentId = null
    );
}
