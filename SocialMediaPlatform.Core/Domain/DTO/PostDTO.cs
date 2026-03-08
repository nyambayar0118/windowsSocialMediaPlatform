using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Post классын объектын Data Transfer Object
    /// </summary>
    /// <param name="Id">ID дугаар</param>
    /// <param name="AuthorId">Post-ийг үүсгэсэн хэрэглэгчийн ID дугаар</param>  
    /// <param name="Visibility">Post-ийн харагдацын түвшин</param>
    /// <param name="CreatedAt">Үүсгэсэн огноо</param>
    /// <param name="Title">Post-ийн гарчиг</param>
    /// <param name="GroupId">Post-ийг оруулсан группын ID дугаар</param>
    public record PostDTO(
        PostId Id,
        UserId AuthorId,
        VisibilityType Visibility,
        DateTime CreatedAt,
        string? Title = null,
        GroupId? GroupId = null
    );
}
