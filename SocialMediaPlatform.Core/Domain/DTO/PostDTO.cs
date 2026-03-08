using SocialMediaPlatform.Core.Domain.Enum;
using SocialMediaPlatform.Core.Domain.ID;

namespace SocialMediaPlatform.Core.Domain.DTO
{
    /// <summary>
    /// Post классын объектын Data Transfer Object
    /// </summary>
    /// <param name="Id">ID дугаар</param>
    /// <param name="AuthorId">Post-ийг үүсгэсэн хэрэглэгчийн ID дугаар</param>
    /// <param name="Type">Post-ийн төрөл</param>
    /// <param name="Visibility">Post-ийн харагдацын түвшин</param>
    /// <param name="CreatedAt">Үүсгэсэн огноо</param>
    public record PostDTO(
        PostId Id,
        UserId AuthorId,
        string Type,
        VisibilityType Visibility,
        DateTime CreatedAt
    );
}
